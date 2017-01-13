//
//  PhotoViewController.m
//  L0Smoothing
//
//  Created by WangDa on 11/10/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#import "PhotoViewController.h"
#import "NSString+StdString.h"
#import "UIImage2OpenCV.h"
#include "MatLib.h"
#include "MorphoFeatures.h"
#include "WatershedSegmenter.h"
#import "AppDelegate.h"
#include "Graphcuts.h"
#include "FaceDetector.h"
#import "UIImage+FiltrrCompositions.h"
#import "DomainTransform.h"

#define kFilterImageViewTag 9999
#define kFilterImageViewContainerViewTag 9998
#define kFilterCellHeight 72.0f

@interface PhotoViewController ()<UITableViewDataSource, UITableViewDelegate>

@property (weak, nonatomic) IBOutlet UIImageView *imageView;
@property (weak, nonatomic) IBOutlet UITableView *tableView;

@property (nonatomic) int width;
@property (nonatomic) int height;

@end

@implementation PhotoViewController

@synthesize imageView;
@synthesize width;
@synthesize height;


using namespace cv;

- (void)viewDidLoad {
    [super viewDidLoad];
    
    _tableView.delegate = self;
    _tableView.dataSource = self;
    _tableView.separatorStyle = UITableViewCellSeparatorStyleNone;
    _tableView.showsVerticalScrollIndicator = NO;
    
    //tableview逆时针旋转90度。
    _tableView.transform = CGAffineTransformMakeRotation(-M_PI / 2);
    
    // scrollbar 不显示
    _tableView.showsVerticalScrollIndicator = NO;
    [self.view addSubview:_tableView];
    
    
    width = 500;
    height = 500;
    
    
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    
    
    //infoLabel.text = [NSString stringWithFormat:@"w:%.0f,h:%.0f", imageView.image.size.width, imageView.image.size.height];
    
    if (delegate.origin == nil)
    {
        delegate.origin = [[UIImage alloc] init];
        delegate.current = [[UIImage alloc] init];

        delegate.origin = imageView.image;
        delegate.current = imageView.image;
    }
    else
    {
        imageView.image = delegate.origin;
        delegate.current = delegate.origin;
    }
    
    
    NSData *data;
    if (UIImagePNGRepresentation(imageView.image) == nil)
    {
        data = UIImageJPEGRepresentation(imageView.image, 1.0);
    }
    else
    {
        data = UIImagePNGRepresentation(imageView.image);
    }
    
    //图片保存的路径
    //这里将图片放在沙盒的documents文件夹中
    NSString * DocumentsPath = [NSHomeDirectory() stringByAppendingPathComponent:@"Documents"];
    
    //文件管理器
    NSFileManager *fileManager = [NSFileManager defaultManager];
    
    //把刚刚图片转换的data对象拷贝至沙盒中 并保存为image.png
    [fileManager createDirectoryAtPath:DocumentsPath withIntermediateDirectories:YES attributes:nil error:nil];
    [fileManager createFileAtPath:[DocumentsPath stringByAppendingString:@"/image.png"] contents:data attributes:nil];
    
    //得到选择后沙盒中图片的完整路径
    filePath = [[NSString alloc]initWithFormat:@"%@%@",DocumentsPath,  @"/image.png"];
    
    
    
    // Do any additional setup after loading the view.
    
    UILongPressGestureRecognizer *longPressGesture = [[UILongPressGestureRecognizer alloc]initWithTarget:self action:@selector(longPressBtn:)];
    [longPressGesture setDelegate:self];
    longPressGesture.minimumPressDuration=0.5;//默认0.5秒
    [imageView addGestureRecognizer:longPressGesture];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}



#pragma mark - Filters TableView Delegate & Datasource methods
- (CGFloat)tableView:(UITableView *)tableView heightForRowAtIndexPath:(NSIndexPath *)indexPath {
    return kFilterCellHeight;
}

- (void)tableView:(UITableView *)tableView didSelectRowAtIndexPath:(NSIndexPath *)indexPath {
    int i =(int)[indexPath row];
    
    NSLog(@"select %d", i);
    
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    
    switch (i) {
        case 0:
        {
            imageView.image = delegate.origin;
            delegate.current = delegate.origin;
            break;
        }
        case 1: // l0smoothing for scene
        {
            // 只有L0会改变 currentImage
            cvImage = [delegate.origin toMat];
            cv::Mat t = L0Smoothing(cvImage, 0.02, 2);
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            
            delegate.current = [self UIImageFromCVMat:t];
            
            imageView.image = delegate.current;
            break;
        }
        case 2: // l0smoothing for face
        {
            // 只有L0会改变 currentImage
            cvImage = [delegate.origin toMat];
            cv::Mat t = L0Smoothing(cvImage, 0.015, 4);
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            
            delegate.current = [self UIImageFromCVMat:t];
            
            imageView.image = delegate.current;
            break;
        }
        case 3: // canny edge
        {
            // Convert UIImage* to cv::Mat
            cvImage = [delegate.current toMat];
            
            cv::Mat gray;
            cv::cvtColor(cvImage, gray, 2);
            // Apply Gaussian filter to remove small edges
            cv::GaussianBlur(gray, gray,
                             cv::Size(5, 5), 1.2, 1.2);
            // Calculate edges with Canny
            cv::Mat edges;
            cv::Canny(gray, edges, 0, 50);
            // Fill image with white color
            cvImage.setTo(cv::Scalar::all(255));
            // Change color on edges
            cvImage.setTo(cv::Scalar(0, 128, 255, 255), edges);
            imageView.image = [UIImage imageWithMat:cvImage andImageOrientation:UIImageOrientationUp];
            
            break;
        }
        case 4: // morpho edge
        {
            cvImage = [delegate.current toMat];
            
            MorphoFeatures morpho;
            morpho.threshold = 40;
            
            cv::Mat grayimage;
            cv::cvtColor(cvImage, grayimage, cv::COLOR_RGB2GRAY);
            
            cv::Mat edges;
            edges = morpho.getEdges(grayimage);
            
            imageView.image = [self UIImageFromCVMat:edges];
            break;
        }
        case 5: // watersheds
        {
            
            cvImage = [delegate.current toMat];
            
            cv::cvtColor(cvImage, cvImage, cv::COLOR_BGRA2BGR);
            
            cv::Mat binary;// = cv::imread(argv[2], 0);
            cv::cvtColor(cvImage, binary, cv::COLOR_BGR2GRAY);
            cv::threshold(binary, binary, 100, 255, THRESH_BINARY);
            
            // Eliminate noise and smaller objects
            cv::Mat fg;
            cv::erode(binary,fg,cv::Mat(),cv::Point(-1,-1),2);
            
            // Identify image pixels without objects
            cv::Mat bg;
            cv::dilate(binary,bg,cv::Mat(),cv::Point(-1,-1),3);
            cv::threshold(bg,bg,1, 128,cv::THRESH_BINARY_INV);
            
            // Create markers image
            cv::Mat markers(binary.size(),CV_8U,cv::Scalar(0));
            markers= fg+bg;
            
            // Create watershed segmentation object
            WatershedSegmenter segmenter;
            segmenter.setMarkers(markers);
            
            cv::Mat result = segmenter.process(cvImage);
            result.convertTo(result,CV_8U);
            
            imageView.image = [self UIImageFromCVMat:result];
            

            break;
        }
        case 6: // graphcut
        {
            double sigma = delegate.sigma;
            // the bigger k the less segmented part
            double k = delegate.k;
            
            // small components removing
            int smallThr = delegate.smallth;
            int segs = 0;
            
            cvImage = [delegate.current toMat];
            
            cv::Mat dst(cvImage.rows, cvImage.cols, CV_8UC3);
            segs = graphCuts(cvImage, dst, sigma, k, smallThr);
            
            NSLog(@"Seg count: %d", segs);
            
            imageView.image = [self UIImageFromCVMat:dst];
            
            break;
        }
        case 7: // face detector
        {
            //NSString *path = [[NSBundle mainBundle] pathForResource:@"haarcascade_eye" ofType:@".xml"];
            NSString *path = [[NSBundle mainBundle] pathForResource:@"haarcascade_frontalface_alt2" ofType:@".xml"];
            NSLog(@"%@",path);
            
            FaceDetector *face = [[FaceDetector alloc]init];
            if (![face initWithFaceCascade:path])
            {
                NSLog(@"Error");
            }
            
            NSArray *faces = [face detectFaces:delegate.current];
            
            NSLog(@"Face count:%lu", (unsigned long)[faces count]);
            
            imageView.image = [face detectFacesDebug:imageView.image];
            

            break;
        }
        case 8: // Domain Transform
        {
            cvImage = [delegate.origin toMat];
            
            cv::Mat t = DomainTransform(cvImage);
            // important!!
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            delegate.current = [self UIImageFromCVMat:t];
            imageView.image = delegate.current;
            
            break;
        }
        case 9:
        {
            UIImage *img = delegate.current;
            img = [img e1];
            imageView.image = img;
            break;
        }
        case 10:
        {
            UIImage *img = delegate.current;
            img = [img e2];
            imageView.image = img;
            break;
        }
        case 11:
        {
            UIImage *img = delegate.current;
            img = [img e3];
            imageView.image = img;
            break;
        }
        case 12:
        {
            UIImage *img = delegate.current;
            img = [img e4];
            imageView.image = img;
            break;
        }
        case 13:
        {
            UIImage *img = delegate.current;
            img = [img e5];
            imageView.image = img;
            break;
        }
        case 14:
        {
            UIImage *img = delegate.current;
            img = [img e6];
            imageView.image = img;
            break;
        }
        case 15:
        {
            UIImage *img = delegate.current;
            img = [img e7];
            imageView.image = img;
            break;
        }
        case 16:
        {
            UIImage *img = delegate.current;
            img = [img e8];
            imageView.image = img;
            break;
        }
        default:
            break;
    }
}

- (UITableViewCell *)tableView:(UITableView *)tableView cellForRowAtIndexPath:(NSIndexPath *)indexPath {
    static NSString *filtersTableViewCellIdentifier = @"filtersTableViewCellIdentifier";
    UITableViewCell *cell = [tableView dequeueReusableCellWithIdentifier: filtersTableViewCellIdentifier];
    UIImageView *filterImageView;
    UIView *filterImageViewContainerView;
    if (cell == nil) {
        cell = [[UITableViewCell alloc] initWithStyle:UITableViewCellStyleDefault reuseIdentifier:filtersTableViewCellIdentifier];
        cell.selectionStyle = UITableViewCellSelectionStyleNone;
        
        filterImageView = [[UIImageView alloc] initWithFrame:CGRectMake(7.5, -7.5, 57, 72)];
        filterImageView.transform = CGAffineTransformMakeRotation(M_PI/2);
        filterImageView.tag = kFilterImageViewTag;
        
        filterImageViewContainerView = [[UIView alloc] initWithFrame:CGRectMake(0, 7, 57, 72)];
        filterImageViewContainerView.tag = kFilterImageViewContainerViewTag;
        [filterImageViewContainerView addSubview:filterImageView];
        
        [cell.contentView addSubview:filterImageViewContainerView];
    } else {
        filterImageView = (UIImageView *)[cell.contentView viewWithTag:kFilterImageViewTag];
    }
    
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    UIImage *img = delegate.origin;
    
    UIGraphicsBeginImageContext(CGSizeMake(57, 72));
    [img drawInRect:CGRectMake(0, 0, 57, 72)];
    img = UIGraphicsGetImageFromCurrentImageContext();
    UIGraphicsEndImageContext();
    
    
    switch ([indexPath row]) {
        case 0: {
            filterImageView.image = img;
            break;
        }
        case 1: {
            // 只有L0会改变 currentImage
            cvImage = [img toMat];
            
            //for scene
            //cv::Mat t = L0Smoothing(cvImage, delegate.lambda, delegate.kappa);
            cv::Mat t = L0Smoothing(cvImage, 0.02, 2);
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            filterImageView.image = [self UIImageFromCVMat:t];
            
            break;
        }
        case 2: {
            // 只有L0会改变 currentImage
            cvImage = [img toMat];
            
            //for face
            //cv::Mat t = L0Smoothing(cvImage, delegate.lambda, delegate.kappa);
            cv::Mat t = L0Smoothing(cvImage, 0.015, 4);
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            filterImageView.image = [self UIImageFromCVMat:t];
            
            break;
        }
        case 3: {
            // Convert UIImage* to cv::Mat
            cvImage = [img toMat];
            
            cv::Mat gray;
            cv::cvtColor(cvImage, gray, 2);
            // Apply Gaussian filter to remove small edges
            cv::GaussianBlur(gray, gray,
                             cv::Size(5, 5), 1.2, 1.2);
            // Calculate edges with Canny
            cv::Mat edges;
            cv::Canny(gray, edges, 0, 50);
            // Fill image with white color
            cvImage.setTo(cv::Scalar::all(255));
            // Change color on edges
            cvImage.setTo(cv::Scalar(0, 128, 255, 255), edges);
            filterImageView.image = [UIImage imageWithMat:cvImage andImageOrientation:UIImageOrientationUp];
            
            break;
        }
        case 4: {
            cvImage = [img toMat];
            
            MorphoFeatures morpho;
            morpho.threshold = 40;
            
            cv::Mat grayimage;
            cv::cvtColor(cvImage, grayimage, cv::COLOR_RGB2GRAY);
            
            cv::Mat edges;
            edges = morpho.getEdges(grayimage);
            filterImageView.image = [self UIImageFromCVMat:edges];
            
            break;
        }
        case 5: {
            
            cvImage = [img toMat];
            
            cv::cvtColor(cvImage, cvImage, cv::COLOR_BGRA2BGR);
            
            cv::Mat binary;// = cv::imread(argv[2], 0);
            cv::cvtColor(cvImage, binary, cv::COLOR_BGR2GRAY);
            cv::threshold(binary, binary, 100, 255, THRESH_BINARY);
            
            // Eliminate noise and smaller objects
            cv::Mat fg;
            cv::erode(binary,fg,cv::Mat(),cv::Point(-1,-1),2);
            
            // Identify image pixels without objects
            cv::Mat bg;
            cv::dilate(binary,bg,cv::Mat(),cv::Point(-1,-1),3);
            cv::threshold(bg,bg,1, 128,cv::THRESH_BINARY_INV);
            
            // Create markers image
            cv::Mat markers(binary.size(),CV_8U,cv::Scalar(0));
            markers= fg+bg;
            
            // Create watershed segmentation object
            WatershedSegmenter segmenter;
            segmenter.setMarkers(markers);
            
            cv::Mat result = segmenter.process(cvImage);
            result.convertTo(result,CV_8U);
            
            filterImageView.image = [self UIImageFromCVMat:result];
            
            break;
        }
        case 6: {
            double sigma = delegate.sigma;
            // the bigger k the less segmented part
            double k = delegate.k;
            
            // small components removing
            int smallThr = delegate.smallth;
            int segs = 0;
            
            cvImage = [img toMat];
            
            cv::Mat dst(cvImage.rows, cvImage.cols, CV_8UC3);
            segs = graphCuts(cvImage, dst, sigma, k, smallThr);
            
            NSLog(@"Seg count: %d", segs);
            
            filterImageView.image = [self UIImageFromCVMat:dst];
            
            break;
        }
        case 7: {
            //NSString *path = [[NSBundle mainBundle] pathForResource:@"haarcascade_eye" ofType:@".xml"];
            NSString *path = [[NSBundle mainBundle] pathForResource:@"haarcascade_frontalface_alt2" ofType:@".xml"];
            NSLog(@"%@",path);
            
            FaceDetector *face = [[FaceDetector alloc]init];
            if (![face initWithFaceCascade:path])
            {
                NSLog(@"Error");
            }
            
            NSArray *faces = [face detectFaces:img];
            
            NSLog(@"Face count:%lu", (unsigned long)[faces count]);
            
            filterImageView.image = [face detectFacesDebug:img];
            
            break;
        }
        case 8: {
            
            cvImage = [img toMat];
            cv::Mat t = DomainTransform(cvImage);
            cv::cvtColor(t, t, cv::COLOR_RGB2BGR);
            filterImageView.image = [self UIImageFromCVMat:t];
            
            break;
        }
        case 9: {
            filterImageView.image = [img e1];
            
            break;
        }
        case 10: {
            filterImageView.image = [img e2];
            
            break;
        }
        case 11: {
            filterImageView.image = [img e3];
            
            break;
        }
        case 12: {
            filterImageView.image = [img e4];
            
            break;
        }
        case 13: {
            filterImageView.image = [img e5];
            
            break;
        }
        case 14: {
            filterImageView.image = [img e6];
            
            break;
        }
        case 15: {
            filterImageView.image = [img e7];
            
            break;
        }
        case 16: {
            filterImageView.image = [img e8];
            
            break;
        }
 
        default: {
            filterImageView.image = img;
            
            break;
        }
    }
    
    
    return cell;
}
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section {
    return 18;
}


#define BYTE unsigned char

cv::Mat L0Smoothing(cv::Mat &im8uc3, double lambda = 2e-2, double kappa = 2.0) {
    
    
    // convert the image to double format
    int row = im8uc3.rows, col = im8uc3.cols;
    
    cv::Mat S;
    im8uc3.convertTo(S, CV_64FC3, 1./255.);
    
    cv::Mat fx(1,2,CV_64FC1);
    cv::Mat fy(2,1,CV_64FC1);
    fx.at<double>(0) = 1; fx.at<double>(1) = -1;
    fy.at<double>(0) = 1; fy.at<double>(1) = -1;
    
    cv::Size sizeI2D = im8uc3.size();
    cv::Mat otfFx = psf2otf(fx, sizeI2D);
    cv::Mat otfFy = psf2otf(fy, sizeI2D);
    
    
    
    cv::Mat Normin1[3];
    cv::Mat single_channel[3];
    cv::split(S, single_channel);
    
    // 这一步结果不一样
    for (int k = 0; k < 3; k++) {
        cv::dft(single_channel[k], Normin1[k], cv::DFT_COMPLEX_OUTPUT);
    }
    
    
    
    cv::Mat Denormin2(row, col, CV_64FC1);
    for (int i = 0; i < row; i++) {
        for (int j = 0; j < col; j++) {
            cv::Vec2d &c1 = otfFx.at<cv::Vec2d>(i,j), &c2 = otfFy.at<cv::Vec2d>(i,j);
            Denormin2.at<double>(i,j) = sqr(c1[0]) + sqr(c1[1]) + sqr(c2[0]) + sqr(c2[1]);
        }
    }
    
    double beta = 5.0*lambda;
    double betamax = 1e5;
    
    while (beta < betamax) {
        cv::Mat Denormin = 1.0 + beta*Denormin2;
        
        // h-v subproblem
        cv::Mat dx[3], dy[3];
        for (int k = 0; k < 3; k++) {
            cv::Mat shifted_x = single_channel[k].clone();
            circshift(shifted_x, 0, -1);
            dx[k] = shifted_x - single_channel[k];
            
            cv::Mat shifted_y = single_channel[k].clone();
            circshift(shifted_y, -1, 0);
            dy[k] = shifted_y - single_channel[k];
        }
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                double val =
                sqr(dx[0].at<double>(i,j)) + sqr(dy[0].at<double>(i,j)) +
                sqr(dx[1].at<double>(i,j)) + sqr(dy[1].at<double>(i,j)) +
                sqr(dx[2].at<double>(i,j)) + sqr(dy[2].at<double>(i,j));
                
                if (val < lambda / beta) {
                    dx[0].at<double>(i,j) = dx[1].at<double>(i,j) = dx[2].at<double>(i,j) = 0.0;
                    dy[0].at<double>(i,j) = dy[1].at<double>(i,j) = dy[2].at<double>(i,j) = 0.0;
                }
            }
        }
        
        // S subproblem
        for (int k = 0; k < 3; k++) {
            cv::Mat shift_dx = dx[k].clone();
            circshift(shift_dx, 0, 1);
            cv::Mat ddx = shift_dx - dx[k];
            
            cv::Mat shift_dy = dy[k].clone();
            circshift(shift_dy, 1, 0);
            cv::Mat ddy = shift_dy - dy[k];
            cv::Mat Normin2 = ddx + ddy;
            cv::Mat FNormin2;
            cv::dft(Normin2, FNormin2, cv::DFT_COMPLEX_OUTPUT);
            cv::Mat FS = Normin1[k] + beta*FNormin2;
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    FS.at<cv::Vec2d>(i,j)[0] /= Denormin.at<double>(i,j);
                    FS.at<cv::Vec2d>(i,j)[1] /= Denormin.at<double>(i,j);
                }
            }
            cv::Mat ifft;
            cv::idft(FS, ifft, cv::DFT_SCALE | cv::DFT_COMPLEX_OUTPUT);
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    single_channel[k].at<double>(i,j) = ifft.at<cv::Vec2d>(i,j)[0];
                }
            }
        }
        beta *= kappa;
    }
    cv::merge(single_channel, 3, S);
    
    cv::Mat rS;
    S.convertTo(rS, CV_8UC3, 255./1.);
    
    return rS;
}


- (cv::Mat)cvMatFromUIImage:(UIImage *)image
{
    CGColorSpaceRef colorSpace = CGImageGetColorSpace(image.CGImage);
    CGFloat cols = image.size.width;
    CGFloat rows = image.size.height;
    
    cv::Mat cvMat(rows, cols, CV_8UC4); // 8 bits per component, 4 channels
    
    CGContextRef contextRef = CGBitmapContextCreate(cvMat.data,                 // Pointer to  data
                                                    cols,                       // Width of bitmap
                                                    rows,                       // Height of bitmap
                                                    8,                          // Bits per component
                                                    cvMat.step[0],              // Bytes per row
                                                    colorSpace,                 // Colorspace
                                                    kCGImageAlphaNoneSkipLast |
                                                    kCGBitmapByteOrderDefault); // Bitmap info flags
    
    CGContextDrawImage(contextRef, CGRectMake(0, 0, cols, rows), image.CGImage);
    CGContextRelease(contextRef);
    CGColorSpaceRelease(colorSpace);
    
    return cvMat;
}

-(UIImage *)UIImageFromCVMat:(cv::Mat)cvMat
{
    NSData *data = [NSData dataWithBytes:cvMat.data length:cvMat.elemSize()*cvMat.total()];
    CGColorSpaceRef colorSpace;
    
    if (cvMat.elemSize() == 1) {
        colorSpace = CGColorSpaceCreateDeviceGray();
    } else {
        colorSpace = CGColorSpaceCreateDeviceRGB();
    }
    
    CGDataProviderRef provider = CGDataProviderCreateWithCFData((__bridge CFDataRef)data);
    
    // Creating CGImage from cv::Mat
    CGImageRef imageRef = CGImageCreate(cvMat.cols,                                 //width
                                        cvMat.rows,                                 //height
                                        8,                                          //bits per component
                                        8 * cvMat.elemSize(),                       //bits per pixel
                                        cvMat.step[0],                            //bytesPerRow
                                        colorSpace,                                 //colorspace
                                        kCGImageAlphaNone|kCGBitmapByteOrderDefault,// bitmap info
                                        provider,                                   //CGDataProviderRef
                                        NULL,                                       //decode
                                        false,                                      //should interpolate
                                        kCGRenderingIntentDefault                   //intent
                                        );
    
    
    // Getting UIImage from CGImage
    UIImage *finalImage = [UIImage imageWithCGImage:imageRef];
    CGImageRelease(imageRef);
    CGDataProviderRelease(provider);
    CGColorSpaceRelease(colorSpace);
    
    return finalImage;
}


-(void)longPressBtn:(UILongPressGestureRecognizer *)gestureRecognizer
{
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    if ([gestureRecognizer state] == UIGestureRecognizerStateBegan) {
        
        imageView.image = delegate.origin;
    }
    else if ([gestureRecognizer state] == UIGestureRecognizerStateEnded) {
        //长按事件结束
        imageView.image = delegate.current;
    }
}


- (IBAction)SavePhoto:(id)sender
{
    UIImageWriteToSavedPhotosAlbum(imageView.image,self,nil,nil);
}



- (IBAction)GetPhoto:(id)sender
{
    UIImagePickerController * picker = [[UIImagePickerController alloc] init];
    picker.delegate = self;
    picker.sourceType = UIImagePickerControllerSourceTypePhotoLibrary;
    [self presentViewController:picker animated:YES completion:^{}];
}


//当选择一张图片后进入这里
-(void)imagePickerController:(UIImagePickerController*)picker didFinishPickingMediaWithInfo:(NSDictionary *)info
{
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    NSString *type = [info objectForKey:UIImagePickerControllerMediaType];
    
    //当选择的类型是图片
    if ([type isEqualToString:@"public.image"])
    {
        //先把图片转成NSData
        UIImage* image = [info objectForKey:@"UIImagePickerControllerOriginalImage"];
        
        // scale
        image = [self scaleImage:image];
        
        NSData *data;
        if (UIImagePNGRepresentation(image) == nil)
        {
            data = UIImageJPEGRepresentation(image, 1.0);
        }
        else
        {
            data = UIImagePNGRepresentation(image);
        }
        
        //图片保存的路径
        //这里将图片放在沙盒的documents文件夹中
        NSString * DocumentsPath = [NSHomeDirectory() stringByAppendingPathComponent:@"Documents"];
        
        //文件管理器
        NSFileManager *fileManager = [NSFileManager defaultManager];
        
        //把刚刚图片转换的data对象拷贝至沙盒中 并保存为image.png
        [fileManager createDirectoryAtPath:DocumentsPath withIntermediateDirectories:YES attributes:nil error:nil];
        [fileManager createFileAtPath:[DocumentsPath stringByAppendingString:@"/image.png"] contents:data attributes:nil];
        
        //得到选择后沙盒中图片的完整路径
        filePath = [[NSString alloc]initWithFormat:@"%@%@",DocumentsPath,  @"/image.png"];
        
        delegate.origin =[UIImage imageWithContentsOfFile:filePath];
        delegate.current = delegate.origin;
        
        imageView.image = delegate.current;
        
        
        [picker dismissViewControllerAnimated:YES completion:^{}];
    } 
    
}

- (UIImage *)scaleImage:(UIImage *)image
{
    // scale the image to about height x width pixel
    float scaleSize = 1.0;
    
    while (1) {
        
        if (image.size.width * scaleSize <= width || image.size.height * scaleSize <= height)
        {
            break;
        }
        scaleSize -= 0.1;
    }
    
    
    UIGraphicsBeginImageContext(CGSizeMake(image.size.width * scaleSize, image.size.height * scaleSize));
    [image drawInRect:CGRectMake(0, 0, image.size.width * scaleSize, image.size.height * scaleSize)];
    UIImage *scaledImage = UIGraphicsGetImageFromCurrentImageContext();
    UIGraphicsEndImageContext();
    
    return scaledImage;
}



/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender {
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
