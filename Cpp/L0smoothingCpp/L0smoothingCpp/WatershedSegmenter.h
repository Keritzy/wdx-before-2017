//
//  WatershedSegmenter.h
//  L0Smoothing
//
//  Created by WangDa on 11/10/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#ifndef L0Smoothing_WatershedSegmenter_h
#define L0Smoothing_WatershedSegmenter_h

#include <stdio.h>
#include "Common.h"

class WatershedSegmenter
{
private:
    cv::Mat markers;
public:
    void setMarkers(cv::Mat& markerImage)
    {
        markerImage.convertTo(markers, CV_32SC1);
    }
    
    cv::Mat process(cv::Mat &image)
    {
        cv::watershed(image, markers);
        markers.convertTo(markers,CV_8UC3);
        return markers;
    }
};

#endif
