//
//  ViewController.m
//  watch-plane-together
//
//  Created by dawang on 16/8/25.
//  Copyright © 2016年 dawang. All rights reserved.
//

#import "ViewController.h"


@interface ViewController (){
    CLLocationManager *_locationManager;
    double _latitude;
    double _longitude;
    double _altitude;
}

@property (weak, nonatomic) IBOutlet UILabel *conditionLabel;
@property (weak, nonatomic) IBOutlet UILabel *watchCountLabel;
@property (weak, nonatomic) IBOutlet UITextView *consoleTextView;
@property (weak, nonatomic) IBOutlet UILabel *statusLabel;
@property (weak, nonatomic) IBOutlet UIButton *statusChangeButton;
@property (nonatomic) Boolean isOnline;
@property (nonatomic) NSString *host;


@end


@implementation ViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    _isOnline = false;
    _host = @"http://10.60.15.48:8080/";
    _statusChangeButton.titleLabel.text = @"上线";
    _statusLabel.text = @"状态：离线";
    
    _locationManager = [[CLLocationManager alloc] init];
    _locationManager.delegate = self;
    
    [_locationManager requestWhenInUseAuthorization];
    [_locationManager startUpdatingLocation];
    
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)statusChangeAction:(id)sender {
    if (_isOnline){
        _isOnline = false;
        [self updateButtonwithTitle:@"上线" andLabel:@"状态：离线"];
        [self consolePaddingwithString:@"已离线"];
        
    } else {
        
        [self consolePaddingwithString:@"尝试上线中..."];
        
        NSString *content = [self sendSyncGetwithURL:@"debug"];
        if ([content isEqualToString:@""]){
            [self consolePaddingwithString:@"上线失败"];
        } else {
            _isOnline = true;
            [self updateButtonwithTitle:@"下线" andLabel:@"状态：上线"];
            [self consolePaddingwithString:content];
        }
        
        
    }
}

- (IBAction)clearConsoleAction:(id)sender {
    [_consoleTextView setText:@"已清空"];
    [_consoleTextView setFont:[UIFont systemFontOfSize:16]];
}

- (IBAction)sendPositionAction:(id)sender {
    [self consolePaddingwithString:@"获取位置中..."];
    [self consolePaddingwithString:[NSString stringWithFormat:@"经度：%f，纬度：%f\n海拔：%f", _longitude, _latitude, _altitude]];
    
    
    [self consolePaddingwithString:@"尝试连接位置服务..."];
    
    NSString *param = [NSString stringWithFormat:@"uid=%d&latitude=%f&longitude=%f&altitude=%f", arc4random() % 1000000, _latitude, _longitude, _altitude];
    //NSString *param = [NSString stringWithFormat:@"uid=%d&latitude=%f&longitude=%f&altitude=%f", 123456, _latitude, _longitude, _altitude];
    NSLog(param);
    
    NSString *content = [self sendSyncPostwitURL:@"debug/position" andParam:param];

    if ([content isEqualToString:@""]){
        [self consolePaddingwithString:@"位置服务连接失败"];
    } else {
        [self consolePaddingwithString:content];
    }
}

- (IBAction)getWeatherAction:(id)sender {
    [self consolePaddingwithString:@"尝试连接天气服务..."];
    NSString *content = [self sendSyncGetwithURL:@"debug/weather"];
    if ([content isEqualToString:@""]){
        [self consolePaddingwithString:@"天气服务连接失败"];
    } else {
        [self consolePaddingwithString:content];
    }
}

- (IBAction)getFlightAction:(id)sender {
    [self consolePaddingwithString:@"尝试连接航线服务..."];
    NSString *content = [self sendSyncGetwithURL:@"debug/flight"];
    if ([content isEqualToString:@""]){
        [self consolePaddingwithString:@"航线服务连接失败"];
    } else {
        [self consolePaddingwithString:content];
    }
}

- (IBAction)getNearAction:(id)sender {
    [self consolePaddingwithString:@"尝试连接附近服务..."];
    NSString *param = [NSString stringWithFormat:@"uid=%d&latitude=%f&longitude=%f&altitude=%f", 123456, _latitude, _longitude, _altitude];
    NSLog(param);
    
    NSString *content = [self sendSyncPostwitURL:@"debug/near" andParam:param];
    
    if ([content isEqualToString:@""]){
        [self consolePaddingwithString:@"附近服务连接失败"];
    } else {
        [self consolePaddingwithString:content];
    }
}

- (void)consolePaddingwithString:(NSString*)content {
    [_consoleTextView setText:[NSString stringWithFormat:@"%@\n%@", content, _consoleTextView.text]];
    [_consoleTextView setFont:[UIFont systemFontOfSize:16]];
}

- (void)updateButtonwithTitle:(NSString*)title andLabel:(NSString*)label {
    [_statusChangeButton setTitle:title forState:UIControlStateNormal];
    [_statusLabel setText: label];
}

- (NSString *)sendSyncGetwithURL:(NSString *)api {
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat:@"%@%@", _host, api]];
    NSURLRequest *request = [[NSURLRequest alloc]initWithURL:url cachePolicy:NSURLRequestReloadIgnoringLocalAndRemoteCacheData timeoutInterval:10];
    NSData *received = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
    NSString *str = [[NSString alloc]initWithData:received encoding:NSUTF8StringEncoding];
    return str;
}

- (NSString *)sendSyncPostwitURL:(NSString *)api andParam:(NSString *)params {
    NSURL *url = [NSURL URLWithString:[NSString stringWithFormat:@"%@%@", _host, api]];
    NSMutableURLRequest *request = [[NSMutableURLRequest alloc]initWithURL:url cachePolicy:NSURLRequestReloadIgnoringLocalAndRemoteCacheData timeoutInterval:10];
    [request setHTTPMethod:@"POST"];
    NSData *data = [params dataUsingEncoding:NSUTF8StringEncoding];
    [request setHTTPBody:data];
    NSData *received = [NSURLConnection sendSynchronousRequest:request returningResponse:nil error:nil];
    NSString *str = [[NSString alloc]initWithData:received encoding:NSUTF8StringEncoding];
    return str;
}

- (void)locationManager:(CLLocationManager *)manager didUpdateLocations:(NSArray<CLLocation *> *)locations{
    CLLocation *location = [locations lastObject];
    // 保存到变量中
    _longitude = location.coordinate.longitude;
    _latitude = location.coordinate.latitude;
    _altitude = location.altitude;
    
    NSLog(@"经度：%f，纬度：%f\n海拔：%f", location.coordinate.longitude, location.coordinate.latitude,location.altitude);
    //[self consolePaddingwithString:[NSString stringWithFormat:@"经度：%f，纬度：%f\n海拔：%f", location.coordinate.longitude, location.coordinate.latitude,location.altitude]];
}

@end
