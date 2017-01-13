//
//  FaceDetector.m
//  FaceDetector
//
//  Created by Tri Nguyen on 6/2/14.
//  Copyright (c) 2014 Tri Nguyen. All rights reserved.
//

#import "FaceDetector.h"
#import "OpenCVUtils.h"

@interface FaceDetector()   {
    cv::CascadeClassifier _faceCascade;
}

@end

@implementation FaceDetector
- (id) initWithFaceCascade:(NSString*) filePath {
    self = [super init];
    if (self)   {
        _faceCascade = cv::CascadeClassifier([filePath UTF8String]);
    }
    return self;
}

- (NSArray*) detectFaces:(UIImage*) image   {
    //detect faces with C code
    std::vector<cv::Rect> faces;
    cv::Mat matImage = [OpenCVUtils cvMatFromUIImage:image];
    cv::Size minSize = cv::Size(20, 20);
    _faceCascade.detectMultiScale(matImage, faces, 1.1, 3, cv::CASCADE_FIND_BIGGEST_OBJECT | cv::CASCADE_DO_ROUGH_SEARCH, minSize);
    
    //convert cv::vector<cv::Rect> to NSArray of CGRect
    NSMutableArray *arrFaces = [[NSMutableArray alloc] init];
    
    for(std::vector<cv::Rect>::iterator it = faces.begin(); it != faces.end(); ++it)
    {
        CGRect rect = [OpenCVUtils cgRectFromCVRect:*it];
        NSValue *value = [NSValue valueWithCGRect:rect];
        [arrFaces addObject:value];
    }
    
    return arrFaces;
}

- (UIImage*) detectFacesDebug:(UIImage*) image   {
    //detect faces with C code
    std::vector<cv::Rect> faces;
    cv::Mat matImage = [OpenCVUtils cvMatFromUIImage:image];
    cv::Mat grayone;
    
    cv::cvtColor(matImage,grayone,cv::COLOR_RGB2GRAY);
    // need to convert to gray?
    
    cv::Size minSize = cv::Size(20, 20);
    _faceCascade.detectMultiScale(grayone, faces, 1.1, 3, cv::CASCADE_FIND_BIGGEST_OBJECT | cv::CASCADE_DO_ROUGH_SEARCH, minSize);
    
    for(std::vector<cv::Rect>::iterator it = faces.begin(); it != faces.end(); ++it)
    {
        // draw the face on the image
        cv::rectangle(matImage, *it, cv::Scalar(255,255,0));
    }
    
    //[OpenCVUtils UIImageFromCVMat:matImage];
    
    
    return [OpenCVUtils UIImageFromCVMat:matImage];;
}

@end
