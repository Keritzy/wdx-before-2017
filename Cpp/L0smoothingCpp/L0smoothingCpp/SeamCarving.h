//
//  SeamCarving.h
//  L0smoothingCpp
//
//  Created by WangDa on 11/28/14.
//  Copyright (c) 2014 DaWang. All rights reserved.
//

#ifndef L0smoothingCpp_SeamCarving_h
#define L0smoothingCpp_SeamCarving_h

#include "Common.h"
using namespace std;

int mode;
int npix;

const int INF = 1 << 16;

void description() {
    cout << "*** Seam Carving ***" << endl;
    cout << "  [1] shrinking" << endl;
    cout << "  [2] enlarging" << endl;
    cout << "  choose mode [1 or 2]: ";
    cin >> mode;
    cout << "  how many pixels?: ";
    cin >> npix;
}

void detectEdge(cv::InputArray img, cv::OutputArray edge) {
    cv::Mat  I = img.getMat();
    cv::Mat& E = edge.getMatRef();
    
    const int width  = I.cols;
    const int height = I.rows;
    
    cv::Mat gray, grad_x, grad_y;
    cv::cvtColor(I, gray, CV_BGR2GRAY);
    cv::Sobel(gray, grad_x, CV_8U, 1, 0);
    cv::Sobel(gray, grad_y, CV_8U, 0, 1);
    
    E = cv::Mat(height, width, CV_8UC1);
    for(int y=0; y<height; y++) {
        for(int x=0; x<width; x++) {
            double gx = grad_x.at<uchar>(y, x);
            double gy = grad_y.at<uchar>(y, x);
            double e = sqrt(gx * gx + gy * gy);
            E.at<uchar>(y, x) = cv::saturate_cast<uchar>(e);
        }
    }
}

void computeSeam(cv::InputArray edge, vector<int>& seam) {
    cv::Mat e = edge.getMat();
    const int width  = e.cols;
    const int height = e.rows;
    
    cv::Mat table = cv::Mat(height, width, CV_32SC1);
    cv::Mat prev  = cv::Mat(height, width, CV_32SC1);
    for(int x=0; x<width; x++) {
        table.at<int>(0, x) = e.at<uchar>(0, x);
        prev.at<int>(0, x) = 0;
    }
    
    for(int y=1; y<height; y++) {
        for(int x=0; x<width; x++) {
            int id = -1;
            int minval = INT_MAX;
            for(int dx=-1; dx<=1; dx++) {
                int xx = x + dx;
                if(xx >= 0 && xx < width) {
                    if(minval > e.at<uchar>(y-1, xx)) {
                        minval = e.at<uchar>(y-1, xx);
                        id = dx;
                    }
                }
            }
            prev.at<int>(y, x)  = id;
            table.at<int>(y, x) = table.at<int>(y-1, x+id);
        }
    }
    
    seam.resize(height);
    int minval = INT_MAX;
    int cur_x  = 0;
    for(int x=0; x<width; x++) {
        if(minval > table.at<int>(height-1, x)) {
            minval = table.at<int>(height-1, x);
            cur_x = x;
        }
    }
    
    int cur_y = height-1;
    while(cur_y >= 0) {
        seam[cur_y] = cur_x;
        cur_x = cur_x + prev.at<int>(cur_y, cur_x);
        cur_y--;
    }
}

template <class T>
void carveSeam(cv::Mat& img, vector<int>& seam) {
    const int width  = img.cols;
    const int height = img.rows;
    const int dim    = img.channels();
    const int depth  = img.depth();
    
    cv::Mat tmp = cv::Mat(height, width-1, CV_MAKETYPE(depth, dim));
    for(int y=0; y<height; y++) {
        for(int x=0; x<width-1; x++) {
            int xx = (x < seam[y]) ? x : x+1;
            for(int c=0; c<dim; c++) {
                tmp.at<T>(y, x*dim+c) = img.at<T>(y, xx*dim+c);
            }
        }
    }
    tmp.convertTo(img, depth);
}

void enlarge(cv::InputArray I, cv::OutputArray O, cv::Mat& seam) {
    cv::Mat  img = I.getMat();
    cv::Mat& out = O.getMatRef();
    const int width  = img.cols;
    const int height = img.rows;
    const int dim    = img.channels();
    
    out = cv::Mat(height, width + npix, CV_8UC3);
    for(int y=0; y<height; y++) {
        int dx = 0;
        for(int x=0; x<width; x++) {
            int xx = min(x + dx, width+npix-1);
            for(int c=0; c<dim; c++) {
                out.at<uchar>(y, xx*dim+c) = img.at<uchar>(y, x*dim+c);
            }
            
            if(seam.at<int>(y, x) == 1) {
                for(int c=0; c<dim; c++) {
                    out.at<uchar>(y, (xx+1)*dim+c) = img.at<uchar>(y, x*dim+c);
                }
                dx++;
            }
        }
    }
}

void SeamCarving(string filename)
{
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);

    cv::imshow("input", img);
    
    int width  = img.cols;
    int height = img.rows;
    printf("width = %d, height = %d\n", width, height);
    
    // 1是缩小，2是变大
    
    mode = 1;
    npix = 90;
    
    cv::Mat out;
    if(mode == 1) {
        cv::namedWindow("output");
        cv::Mat edge;
        vector<int> seam;
        img.convertTo(out, CV_8U);
        for(int i=0; i<npix; i++) {
            detectEdge(out, edge);
            
            computeSeam(edge, seam);
            
            carveSeam<uchar>(out, seam);
            
            cv::imshow("output", out);
            cv::waitKey(30);
            printf("%3d seams are carved!\r", i+1);
        }
       	printf("\n");
    }
    else {
        cv::Mat idx = cv::Mat(height, width, CV_32SC2);
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                idx.at<int>(y, x*2+0) = x;
                idx.at<int>(y, x*2+1) = y;
            }
        }
        
        cv::Mat edge;
        vector<int> seam;
        img.convertTo(out, CV_8U);
        for(int i=0; i<npix; i++) {

            detectEdge(out, edge);
            
            computeSeam(edge, seam);
            
            carveSeam<uchar>(out, seam);
            carveSeam<int>(idx, seam);
        }
        
        cv::Mat seams = cv::Mat::ones(img.size(), CV_32SC1);
        for(int y=0; y<idx.rows; y++) {
            for(int x=0; x<idx.cols; x++) {
                int xx = idx.at<int>(y, x*2+0);
                int yy = idx.at<int>(y, x*2+1);
                seams.at<int>(yy, xx) = 0;
            }
        }
        
        enlarge(img, out, seams);
    }
    
    cv::imshow("output", out);
}

#endif
