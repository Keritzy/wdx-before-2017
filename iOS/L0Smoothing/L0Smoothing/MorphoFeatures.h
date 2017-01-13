//
//  MorphoFeatures.h
//  L0smoothingCpp
//
//  Created by WangDa on 11/10/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#ifndef __L0smoothingCpp__MorphoFeatures__
#define __L0smoothingCpp__MorphoFeatures__

#include <stdio.h>


class MorphoFeatures
{
public:
    // threshold to produce binary image
    int threshold;
    

    cv::Mat getEdges(const cv::Mat &image)
    {
        // Get the gradien image
        cv::Mat result;
        cv::morphologyEx(image, result, cv::MORPH_GRADIENT, cv::Mat());
        // apply threshold to obtain a binary image
        applyThreshold(result);
        
        return result;
    }
    
    void applyThreshold(cv::Mat& result) {
        // Apply threshold on result
        if (threshold > 0)
            cv::threshold(result, result, threshold, 255, cv::THRESH_BINARY);
    }
    
};


#endif /* defined(__L0smoothingCpp__MorphoFeatures__) */
