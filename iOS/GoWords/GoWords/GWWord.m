//
//  GWWord.m
//  GoWords
//
//  Created by Da Wang on 6/5/14.
//  Copyright (c) 2014 wdxtub. All rights reserved.
//

#import "GWWord.h"

@implementation GWWord

- (id)initWithSpelling:(NSString *)spelling Detail:(NSString *)detail Index:(int)index
{
    self.Spelling = spelling;
    self.Detail = detail;
    self.Index = index;
    
    return self;
}

@end
