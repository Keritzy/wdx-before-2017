//
//  SettingViewController.m
//  L0Smoothing
//
//  Created by WangDa on 11/11/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#import "SettingViewController.h"
#import "AppDelegate.h"

@interface SettingViewController ()

@property (weak, nonatomic) IBOutlet UITextField *lambdaInput;
@property (weak, nonatomic) IBOutlet UITextField *kappaInput;
@property (weak, nonatomic) IBOutlet UITextField *sigmaInput;
@property (weak, nonatomic) IBOutlet UITextField *kInput;
@property (weak, nonatomic) IBOutlet UITextField *smallthInput;


@end

@implementation SettingViewController

@synthesize lambdaInput;
@synthesize kappaInput;
@synthesize sigmaInput;
@synthesize kInput;
@synthesize smallthInput;

- (void)viewDidLoad
{
    [super viewDidLoad];
    
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    
    lambdaInput.text = [NSString stringWithFormat:@"%f",delegate.lambda];
    kappaInput.text = [NSString stringWithFormat:@"%f",delegate.kappa];
    sigmaInput.text = [NSString stringWithFormat:@"%f",delegate.sigma];
    kInput.text = [NSString stringWithFormat:@"%f",delegate.k];
    smallthInput.text = [NSString stringWithFormat:@"%d",delegate.smallth];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)saveParameter:(id)sender
{
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    
    delegate.lambda = [lambdaInput.text doubleValue];
    delegate.kappa = [kappaInput.text doubleValue];
    delegate.sigma = [sigmaInput.text doubleValue];
    delegate.k = [kInput.text doubleValue];
    delegate.smallth = [smallthInput.text intValue];
    
    NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
    
    [ud setObject:lambdaInput.text forKey:@"lambda"];
    [ud setObject:kappaInput.text forKey:@"kappa"];
    [ud setObject:sigmaInput.text forKey:@"sigma"];
    [ud setObject:kInput.text forKey:@"k"];
    [ud setObject:smallthInput.text forKey:@"smallth"];
    
    
    UIAlertView *alertView = [[UIAlertView alloc]initWithTitle:@"Notice" message:@"Parameter Saved" delegate:self cancelButtonTitle:@"OK" otherButtonTitles: nil];
    
    [alertView show];
}

- (IBAction)resetParameter:(id)sender
{
    AppDelegate *delegate = (AppDelegate*)[[UIApplication sharedApplication] delegate];
    
    NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
    
    [ud setObject:@"0.015" forKey:@"lambda"];
    [ud setObject:@"6" forKey:@"kappa"];
    [ud setObject:@"0.8" forKey:@"sigma"];
    [ud setObject:@"440" forKey:@"k"];
    [ud setObject:@"300" forKey:@"smallth"];
    
    delegate.lambda = [[ud objectForKey:@"lambda"] doubleValue];
    delegate.kappa = [[ud objectForKey:@"kappa"] doubleValue];
    delegate.sigma = [[ud objectForKey:@"sigma"] doubleValue];
    delegate.k = [[ud objectForKey:@"k"] doubleValue];
    delegate.smallth = [[ud objectForKey:@"smallth"] intValue];

    
    
    lambdaInput.text = [NSString stringWithFormat:@"%f",delegate.lambda];
    kappaInput.text = [NSString stringWithFormat:@"%f",delegate.kappa];
    sigmaInput.text = [NSString stringWithFormat:@"%f",delegate.sigma];
    kInput.text = [NSString stringWithFormat:@"%f",delegate.k];
    smallthInput.text = [NSString stringWithFormat:@"%d",delegate.smallth];
    
    UIAlertView *alertView = [[UIAlertView alloc]initWithTitle:@"Notice" message:@"Reset Success" delegate:self cancelButtonTitle:@"OK" otherButtonTitles: nil];
    
    [alertView show];
}

- (void)touchesEnded:(NSSet *)touches withEvent:(UIEvent *)event {
    if (![lambdaInput isExclusiveTouch]) {
        [lambdaInput resignFirstResponder];
    }
    
    if (![kappaInput isExclusiveTouch]) {
        [kappaInput resignFirstResponder];
    }
    
    if (![sigmaInput isExclusiveTouch]) {
        [sigmaInput resignFirstResponder];
    }
    
    if (![kInput isExclusiveTouch]) {
        [kInput resignFirstResponder];
    }
    
    if (![smallthInput isExclusiveTouch]) {
        [smallthInput resignFirstResponder];
    }
    
    
    
    
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
