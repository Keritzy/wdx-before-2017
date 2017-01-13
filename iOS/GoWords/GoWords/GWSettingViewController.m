//
//  GWSettingViewController.m
//  GoWords
//
//  Created by Da Wang on 6/22/14.
//  Copyright (c) 2014 wdxtub. All rights reserved.
//

#import "GWSettingViewController.h"

@interface GWSettingViewController ()

@end

@implementation GWSettingViewController

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization
    }
    return self;
}
- (IBAction)reset:(id)sender
{
    NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
    [ud setObject:@"" forKey:@"outlist"];
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"重置成功" message:@"下次打开应用又会从第一个单词开始啦" delegate:self cancelButtonTitle:@"我知道了" otherButtonTitles:nil];
    [alert show];
}

- (IBAction)email:(id)sender
{
    Class mailClass = (NSClassFromString(@"MFMailComposeViewController"));

    if ([mailClass canSendMail]) {
        [self displayMailPicker];
    }
    else
    {
        UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"出问题啦" message:@"你还没有设定邮箱账户哦" delegate:self cancelButtonTitle:@"我知道了" otherButtonTitles:nil];
        [alert show];
    }
}

//调出邮件发送窗口
- (void)displayMailPicker
{
    MFMailComposeViewController *mailPicker = [[MFMailComposeViewController alloc] init];
    mailPicker.mailComposeDelegate = self;
    
    //设置主题
    [mailPicker setSubject: @"GoWords v1.0问题反馈"];
    //添加收件人
    NSArray *toRecipients = [NSArray arrayWithObject: @"wdxtub@163.com"];
    [mailPicker setToRecipients: toRecipients];
    //添加抄送
    //NSArray *ccRecipients = [NSArray arrayWithObjects:@"second@example.com", @"third@example.com", nil];
    //[mailPicker setCcRecipients:ccRecipients];
    //添加密送
    //NSArray *bccRecipients = [NSArray arrayWithObjects:@"fourth@example.com", nil];
    //[mailPicker setBccRecipients:bccRecipients];
    
    // 添加一张图片
    //UIImage *addPic = [UIImage imageNamed: @"Icon@2x.png"];
    //NSData *imageData = UIImagePNGRepresentation(addPic);            // png
    //关于mimeType：http://www.iana.org/assignments/media-types/index.html
    //[mailPicker addAttachmentData: imageData mimeType: @"" fileName: @"Icon.png"];
    
    //添加一个pdf附件
    //NSString *file = [self fullBundlePathFromRelativePath:@"高质量C++编程指南.pdf"];
    //NSData *pdf = [NSData dataWithContentsOfFile:file];
    //[mailPicker addAttachmentData: pdf mimeType: @"" fileName: @"高质量C++编程指南.pdf"];
    
    NSString *emailBody = @"<font color='red'>eMail</font> 正文: 请写下您的意见和建议吧~";
    [mailPicker setMessageBody:emailBody isHTML:YES];
    [self presentViewController:mailPicker animated:YES completion:^{
        
        
        
    }];

}

- (void)mailComposeController:(MFMailComposeViewController *)controller didFinishWithResult:(MFMailComposeResult)result error:(NSError *)error
{
    
    //关闭邮件发送窗口
    NSString *title = @"邮件发送提醒";
    NSString *msg;
    switch (result){
        case MFMailComposeResultCancelled:
            msg = @"邮件已被取消";
            break;
        case MFMailComposeResultSaved:
            msg = @"邮件保存成功";
            [self alertWithTitle:title msg:msg];
            break;
        case MFMailComposeResultSent:
            msg = @"邮件发送成功";
            [self alertWithTitle:title msg:msg];
            break;
        case MFMailComposeResultFailed:
            msg =@"邮件发送失败";
            [self alertWithTitle:title msg:msg];
            break;
    }
    
    [self dismissViewControllerAnimated:YES
                             completion:^(void){
                                 // Code
                             }];
    
}

- (void) alertWithTitle:(NSString *)_title_ msg: (NSString *)msg
{
    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:_title_
                                                    message:msg
                                                   delegate:nil
                                          cancelButtonTitle:@"我知道啦"
                                          otherButtonTitles:nil];
    [alert show];
}


- (IBAction)rate:(id)sender
{
    NSString *str = @"";
    
    if( ([[[UIDevice currentDevice] systemVersion] doubleValue]>=7.0))
    {
        str = [NSString stringWithFormat:@"itms-apps://itunes.apple.com/app/id%@",@"892097483"];
    }
    else
    {
        str = [NSString stringWithFormat:@"itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id=%@",@"892097483" ];
    }
    
    [[UIApplication sharedApplication] openURL:[NSURL URLWithString:str]];
    

}

- (void)viewDidLoad
{
    [super viewDidLoad];
    // Do any additional setup after loading the view.
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

/*
#pragma mark - Navigation

// In a storyboard-based application, you will often want to do a little preparation before navigation
- (void)prepareForSegue:(UIStoryboardSegue *)segue sender:(id)sender
{
    // Get the new view controller using [segue destinationViewController].
    // Pass the selected object to the new view controller.
}
*/

@end
