//
//  AppDelegate.h
//  L0Smoothing
//
//  Created by WangDa on 11/10/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface AppDelegate : UIResponder <UIApplicationDelegate>

@property (strong, nonatomic) UIWindow *window;

@property (nonatomic) double lambda;
@property (nonatomic) double kappa;
@property (strong, nonatomic) UIImage *origin;
@property (strong, nonatomic) UIImage *current;
@property (nonatomic) double sigma;
@property (nonatomic) double k;
@property (nonatomic) int smallth;



@end

