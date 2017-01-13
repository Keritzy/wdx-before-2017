//
//  GWViewController.m
//  GoWords
//
//  Created by Da Wang on 6/5/14.
//  Copyright (c) 2014 wdxtub. All rights reserved.
//

#import "GWViewController.h"
#import "GWAppDelegate.h"
#import "GWWord.h"

@interface GWViewController ()

@property (weak, nonatomic) IBOutlet UILabel *WordLabel;
@property (nonatomic) BOOL isSpelling;
@property (nonatomic) int detailIndex;
@property (weak, nonatomic) IBOutlet UITextView *DetailText;
@property (weak, nonatomic) IBOutlet UILabel *NextFlag1;
@property (weak, nonatomic) IBOutlet UILabel *NextFlag2;
@property (weak, nonatomic) IBOutlet UILabel *NextFlag3;
@property (weak, nonatomic) IBOutlet UILabel *NextFlag4;

@property (strong, nonatomic) NSMutableArray *currentWords;
@property (strong, nonatomic) NSMutableArray *outList;
@property (nonatomic) int listIndex;
@property (nonatomic) int wordIndex;

@end

@implementation GWViewController

- (void)viewDidLoad
{
    [super viewDidLoad];
	// Do any additional setup after loading the view, typically from a nib.
    
    NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
    
    NSString *outvalue = [ud objectForKey:@"outlist"];
    self.outList = [[NSMutableArray alloc] init];
    if (outvalue)
    {
        NSArray *arr = [outvalue componentsSeparatedByString:@"#"];
        for (int i = 0 ; i < [arr count]-1; i++) {
            [self.outList addObject:arr[i]];
        }
    }
    
    //生成用于本次显示的词汇列表,根据排除列表进行筛选
    GWAppDelegate *delegate = (GWAppDelegate *)[[UIApplication sharedApplication]delegate];
    self.currentWords = [[NSMutableArray alloc] init];
    self.wordIndex = 0;
    // 设置一组多少个数
    while ([self.currentWords count] < 50)
    {
        if ([self.outList containsObject: [NSString stringWithFormat:@"%d",self.wordIndex]])
        {
            self.wordIndex++;
            continue;
        }
        else
        {
            [self.currentWords addObject:delegate.WORDS[self.wordIndex]];
            self.wordIndex++;
        }
        
    }
    
    self.listIndex = 0;
    GWWord *word = self.currentWords[self.listIndex];
    
    self.isSpelling = TRUE;
    self.DetailText.hidden = TRUE;
    self.NextFlag1.hidden = TRUE;
    self.NextFlag2.hidden = TRUE;
    self.NextFlag3.hidden = TRUE;
    self.NextFlag4.hidden = TRUE;
    
    
    self.WordLabel.text = word.Spelling;
    self.title = word.Spelling;
    
}

- (IBAction)Next:(id)sender
{
    // 把当前的序号加入排除列表，并写入存储
    GWWord *word = self.currentWords[self.listIndex];
    [self.outList addObject:[NSString stringWithFormat:@"%d",word.Index]];
    
    NSUserDefaults *ud = [NSUserDefaults standardUserDefaults];
    NSMutableString *outstr = [[NSMutableString alloc]init];
    for (NSString* str in self.outList) {
        [outstr appendString:str];
        [outstr appendString:@"#"];
    }
    [ud setObject:outstr forKey:@"outlist"];
    // 把这个词从currentwords里删掉
    [self.currentWords removeObjectAtIndex:self.listIndex];
    // 再新加一个
    GWAppDelegate *delegate = (GWAppDelegate *)[[UIApplication sharedApplication]delegate];
    [self.currentWords addObject:delegate.WORDS[self.wordIndex]];
    self.wordIndex++;
    
    
    self.WordLabel.hidden = FALSE;
    self.DetailText.hidden = TRUE;
    self.NextFlag1.hidden = TRUE;
    self.NextFlag2.hidden = TRUE;
    self.NextFlag3.hidden = TRUE;
    self.NextFlag4.hidden = TRUE;
    self.detailIndex = 0;
    
    word = self.currentWords[self.listIndex];
    self.WordLabel.text = word.Spelling;
    self.title = word.Spelling;

}

- (IBAction)Tap:(id)sender
{
    GWWord *word = self.currentWords[self.listIndex];

    NSArray *arr = [word.Detail componentsSeparatedByString:@"#"];
    self.WordLabel.hidden = TRUE;
    self.DetailText.hidden = FALSE;
    self.NextFlag1.hidden = TRUE;
    self.NextFlag2.hidden = TRUE;
    self.NextFlag3.hidden = TRUE;
    self.NextFlag4.hidden = TRUE;
    
    
    if (self.detailIndex < [arr count])
    {
        self.DetailText.text = (NSString *)arr[self.detailIndex];
        self.DetailText.font = [UIFont systemFontOfSize:28];
        self.detailIndex++;
    }
    else if (self.detailIndex == [arr count])
    {
        self.NextFlag1.hidden = FALSE;
        self.NextFlag2.hidden = FALSE;
        self.NextFlag3.hidden = FALSE;
        self.NextFlag4.hidden = FALSE;
        self.DetailText.text = (NSString *)arr[0];
        self.DetailText.font = [UIFont systemFontOfSize:28];
        self.detailIndex++;
    }
    else
    {
        // 释义显示完，准备显示下一个单词
        self.WordLabel.hidden = FALSE;
        self.DetailText.hidden = TRUE;
        self.detailIndex = 0;
        self.listIndex++;
        
        if (self.listIndex >= [self.currentWords count]) {
            self.listIndex = 0;
        }
        
        word = self.currentWords[self.listIndex];
        self.WordLabel.text = word.Spelling;
        self.title = word.Spelling;
    }
        

    
}

- (void)didReceiveMemoryWarning
{
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
