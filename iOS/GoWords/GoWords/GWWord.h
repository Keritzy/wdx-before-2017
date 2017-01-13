//
//  GWWord.h
//  GoWords
//
//  Created by Da Wang on 6/5/14.
//  Copyright (c) 2014 wdxtub. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface GWWord : NSObject

@property(strong, nonatomic) NSString *Spelling;
@property(strong, nonatomic) NSString *Detail;
@property(nonatomic) int Index;

- (id) initWithSpelling:(NSString *)spelling Detail:(NSString *)detail Index:(int)index;


@end
