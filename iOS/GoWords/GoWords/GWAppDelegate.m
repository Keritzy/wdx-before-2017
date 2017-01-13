//
//  GWAppDelegate.m
//  GoWords
//
//  Created by Da Wang on 6/5/14.
//  Copyright (c) 2014 wdxtub. All rights reserved.
//

#import "GWAppDelegate.h"
#import "GWWord.h"

@implementation GWAppDelegate

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    // Override point for customization after application launch.
    // 载入词汇列表
    [self initWords];
    
    return YES;
}


- (void)initWords
{
    NSString *wordslist = [NSString stringWithContentsOfFile:[[NSBundle mainBundle]pathForResource:@"gre.data" ofType:nil inDirectory:nil] encoding:NSUTF8StringEncoding error:nil];
    
    self.WORDS = [[NSMutableArray alloc] init];
    
    NSArray *wordsarr = [wordslist componentsSeparatedByString:@"\n"];
    for (int i = 0; i < [wordsarr count]-1; i++) {
        NSArray *arr = [wordsarr[i] componentsSeparatedByString:@"@"];
        
        
        GWWord *word = [[GWWord alloc] initWithSpelling:arr[0] Detail:arr[1] Index:i];
        [self.WORDS addObject:word];
    }
}


- (void)applicationWillResignActive:(UIApplication *)application
{
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
    // 去掉通知
    application.applicationIconBadgeNumber = 0;
    [[UIApplication sharedApplication] cancelAllLocalNotifications];
    [self addNotification];
}

- (void)addNotification
{
    UILocalNotification *notification=[[UILocalNotification alloc] init];
    if (notification!=nil)
    {
        NSDate *now = [NSDate date];
        // 设置提醒时间，倒计时以秒为单位。以下是从现在开始5秒以后通知,22小时79200s
        notification.fireDate=[now dateByAddingTimeInterval:79200];
        // 设置时区，使用本地时区
        notification.timeZone=[NSTimeZone defaultTimeZone];
        // 设置提示的文字
        notification.alertBody=@"别忘了跑步和背单词呢！";
        // 设置提示音，使用默认的
        notification.soundName= UILocalNotificationDefaultSoundName;
        // 锁屏后提示文字，一般来说，都会设置与alertBody一样
        notification.alertAction=NSLocalizedString(@"跑步和背单词！", nil);
        // 这个通知到时间时，你的应用程序右上角显示的数字. 获取当前的数字+1
        notification.applicationIconBadgeNumber = [[[UIApplication sharedApplication] scheduledLocalNotifications] count]+1;
        //给这个通知增加key 便于半路取消。nfkey这个key是自己随便写的，还有notificationtag也是自己定义的ID。假如你的通知不会在还没到时间的时候手动取消，那下面的两行代码你可以不用写了。取消通知的时候判断key和ID相同的就是同一个通知了。
        // NSDictionary *dict =[NSDictionary dictionaryWithObjectsAndKeys:[NSNumber numberWithInt:notificationtag],@"nfkey",nil];
        // [notification setUserInfo:dict];
        // 启用这个通知
        [[UIApplication sharedApplication]   scheduleLocalNotification:notification];
        // 创建了就要学会释放。如果不加这一句，通知到时间了，发现顶部通知栏提示的地方有了，然后你通过通知栏进去，然后你发现通知栏里边还有这个提示，除非你手动清除
    }
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
}

@end
