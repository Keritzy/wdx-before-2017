//
//  DomainTransform.h
//  L0smoothingCpp
//
//  Created by WangDa on 11/28/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//
/***********************************************************
 * Domain transform filtering implementation
 ************************************************************
 * This code is an implementation of the paper [Gastal and Oliveira 2011].
 * This paper proposes an edge-preserving filter,
 * which is effectively parallelizable. This filter transforms
 * the domain of the filter function, and performs linear filtering
 * to the transfomed domain.
 ************************************************************/


#include <opencv2/opencv.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <iostream>
#include <vector>
#include <ctime>
#include <list>

using namespace std;

#ifndef L0smoothingCpp_DomainTransform_h
#define L0smoothingCpp_DomainTransform_h

long t, elapsed;
#define CLOCK_START t = clock();
#define CLOCK_END cout << "[ Time ]" << endl << "  " << ((clock() - t) / 1000.0) << " sec" << endl;

// Recursive filter for vertical direction
void recursiveFilterVertical(cv::Mat& out, cv::Mat& dct, double sigma_H) {
    int width  = out.cols;
    int height = out.rows;
    int dim    = out.channels();
    double a   = exp(-sqrt(2.0) / sigma_H);
    
    cv::Mat V;
    dct.convertTo(V, CV_64FC1);
    for(int x=0; x<width; x++) {
        for(int y=0; y<height-1; y++) {
            V.at<double>(y, x) = pow(a, V.at<double>(y, x));
        }
    }
    
    // if openmp is available, compute in parallel

    for(int x=0; x<width; x++) {
        for(int y=1; y<height; y++) {
            double p = V.at<double>(y-1, x);
            for(int c=0; c<dim; c++) {
                double val1 = out.at<double>(y, x*dim+c);
                double val2 = out.at<double>(y-1, x*dim+c);
                out.at<double>(y, x*dim+c) = val1 + p * (val2 - val1);
            }
        }
        
        for(int y=height-2; y>=0; y--) {
            double p = V.at<double>(y, x);
            for(int c=0; c<dim; c++) {
                double val1 = out.at<double>(y, x*dim+c);
                double val2 = out.at<double>(y+1, x*dim+c);
                out.at<double>(y, x*dim+c) = val1 + p * (val2 - val1);
            }
        }
    }
}

// Recursive filter for horizontal direction
void recursiveFilterHorizontal(cv::Mat& out, cv::Mat& dct, double sigma_H) {
    int width  = out.cols;
    int height = out.rows;
    int dim    = out.channels();
    double a = exp(-sqrt(2.0) / sigma_H);
    
    cv::Mat V;
    dct.convertTo(V, CV_64FC1);
    for(int x=0; x<width-1; x++) {
        for(int y=0; y<height; y++) {
            V.at<double>(y, x) = pow(a, V.at<double>(y, x));
        }
    }
    

    for(int y=0; y<height; y++) {
        for(int x=1; x<width; x++) {
            double p = V.at<double>(y, x-1);
            for(int c=0; c<dim; c++) {
                double val1 = out.at<double>(y, x*dim+c);
                double val2 = out.at<double>(y, (x-1)*dim+c);
                out.at<double>(y, x*dim+c) = val1 + p * (val2 - val1);
            }
        }
        
        for(int x=width-2; x>=0; x--) {
            double p = V.at<double>(y, x);
            for(int c=0; c<dim; c++) {
                double val1 = out.at<double>(y, x*dim+c);
                double val2 = out.at<double>(y, (x+1)*dim+c);
                out.at<double>(y, x*dim+c) = val1 + p * (val2 - val1);
            }
        }
    }
}

// Domain transform filtering
void domainTransformFilter(cv::Mat& img, cv::Mat& out, cv::Mat& joint, double sigma_s, double sigma_r, int maxiter) {
    assert(img.depth() == CV_64F && joint.depth() == CV_64F);
    
    int width = img.cols;
    int height = img.rows;
    int dim = img.channels();
    
    // compute derivatives of transformed domain "dct"
    // and a = exp(-sqrt(2) / sigma_H) to the power of "dct"
    cv::Mat dctx = cv::Mat(height, width-1, CV_64FC1);
    cv::Mat dcty = cv::Mat(height-1, width, CV_64FC1);
    double ratio = sigma_s / sigma_r;
    
    for(int y=0; y<height; y++) {
        for(int x=0; x<width-1; x++) {
            double accum = 0.0;
            for(int c=0; c<dim; c++) {
                accum += abs(joint.at<double>(y, (x+1)*dim+c) - joint.at<double>(y, x*dim+c));
            }
            dctx.at<double>(y, x) = 1.0 + ratio * accum;
        }
    }
    
    for(int x=0; x<width; x++) {
        for(int y=0; y<height-1; y++) {
            double accum = 0.0;
            for(int c=0; c<dim; c++) {
                accum += abs(joint.at<double>(y+1, x*dim+c) - joint.at<double>(y, x*dim+c));
            }
            dcty.at<double>(y, x) = 1.0 + ratio * accum;
        }
    }
    
    // Apply recursive folter maxiter times
    img.convertTo(out, CV_MAKETYPE(CV_64F, dim));
    for(int i=0; i<maxiter; i++) {
        double sigma_H = sigma_s * sqrt(3.0) * pow(2.0, maxiter - i - 1) / sqrt(pow(4.0, maxiter) - 1.0);
        recursiveFilterHorizontal(out, dctx, sigma_H);
        recursiveFilterVertical(out, dcty, sigma_H);
    }
    out.convertTo(out, CV_32F);
}

cv::Mat DomainTransform(cv::Mat img)
{
    // change depth
    img.convertTo(img, CV_64FC3, 1.0 / 255.0);
    
    // Parameter set
    const double sigma_s = 25.0;
    const double sigma_r = 0.2;
    const int    maxiter = 10;
    
    cout << "[ Parameters ]" << endl;
    cout << "  * sigma_s = " << sigma_s << endl;
    cout << "  * sigma_r = " << sigma_r << endl;
    cout << "  * maxiter = " << maxiter << endl;
    cout << endl;
    
    // Call domain transform filter
    CLOCK_START
    cv::Mat out;
    domainTransformFilter(img, out, img, sigma_s, sigma_r, maxiter);
    CLOCK_END
    
    // important!! get the pixel back  to origin color
    cv::Mat rS;
    out.convertTo(rS, CV_8UC3, 255./1.);
    
    return rS;
}


#endif



