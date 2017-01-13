//
//  PhotoViewController.h
//  L0Smoothing
//
//  Created by WangDa on 11/10/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface PhotoViewController : UIViewController<UIActionSheetDelegate,UIImagePickerControllerDelegate,UINavigationControllerDelegate, UIGestureRecognizerDelegate>
{
    //下拉菜单
    UIActionSheet *myActionSheet;

    //图片2进制路径
    NSString* filePath;
    
    cv::Mat cvImage;
}
@end
