#include <iostream>
#include <fstream>
#include <vector>
#include "MatLib.h"
#include "Common.h"
#include "MorphoFeatures.h"
#include "Graphcuts.h"
#include "clcnst.h"
#include "LocalLaplacianFilter.h"
#include "DomainTransform.h"
#include "ColorTransfer.h"
#include "SeamCarving.h"
#include "KMeanspp.h"

using namespace std;

#define BYTE unsigned char

string filename = "4.jpg";

cv::Mat L0Smoothing(cv::Mat &im8uc3, double lambda = 2e-2, double kappa = 6.0) {
    // convert the image to double format
    int row = im8uc3.rows, col = im8uc3.cols;
    cv::Mat S;
    im8uc3.convertTo(S, CV_64FC3, 1./255.);
    
    cv::Mat fx(1,2,CV_64FC1);
    cv::Mat fy(2,1,CV_64FC1);
    fx.at<double>(0) = 1; fx.at<double>(1) = -1;
    fy.at<double>(0) = 1; fy.at<double>(1) = -1;
    
    cv::Size sizeI2D = im8uc3.size();
    cv::Mat otfFx = psf2otf(fx, sizeI2D);
    cv::Mat otfFy = psf2otf(fy, sizeI2D);
    
    
    cv::Mat Normin1[3];
    cv::Mat single_channel[3];
    cv::split(S, single_channel);

    
    for (int k = 0; k < 3; k++) {
        cv::dft(single_channel[k], Normin1[k], cv::DFT_COMPLEX_OUTPUT);
    }
    
    
    cv::Mat Denormin2(row, col, CV_64FC1);
    for (int i = 0; i < row; i++) {
        for (int j = 0; j < col; j++) {
            cv::Vec2d &c1 = otfFx.at<cv::Vec2d>(i,j), &c2 = otfFy.at<cv::Vec2d>(i,j);
            Denormin2.at<double>(i,j) = sqr(c1[0]) + sqr(c1[1]) + sqr(c2[0]) + sqr(c2[1]);
        }
    }
    
    // the bigger beta the more time iteration
    double beta = 4.0*lambda;
    // the smaller betamax the less segmentation count
    double betamax = 3e6;
    
    while (beta < betamax) {
        cv::Mat Denormin = 1.0 + beta*Denormin2;
        
        // h-v subproblem
        cv::Mat dx[3], dy[3];
        for (int k = 0; k < 3; k++) {
            cv::Mat shifted_x = single_channel[k].clone();
            circshift(shifted_x, 0, -1);
            dx[k] = shifted_x - single_channel[k];
            
            cv::Mat shifted_y = single_channel[k].clone();
            circshift(shifted_y, -1, 0);
            dy[k] = shifted_y - single_channel[k];
        }
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < col; j++) {
                double val =
                sqr(dx[0].at<double>(i,j)) + sqr(dy[0].at<double>(i,j)) +
                sqr(dx[1].at<double>(i,j)) + sqr(dy[1].at<double>(i,j)) +
                sqr(dx[2].at<double>(i,j)) + sqr(dy[2].at<double>(i,j));
                
                if (val < lambda / beta) {
                    dx[0].at<double>(i,j) = dx[1].at<double>(i,j) = dx[2].at<double>(i,j) = 0.0;
                    dy[0].at<double>(i,j) = dy[1].at<double>(i,j) = dy[2].at<double>(i,j) = 0.0;
                }
            }
        }
        
        // S subproblem
        for (int k = 0; k < 3; k++) {
            cv::Mat shift_dx = dx[k].clone();
            circshift(shift_dx, 0, 1);
            cv::Mat ddx = shift_dx - dx[k];
            
            cv::Mat shift_dy = dy[k].clone();
            circshift(shift_dy, 1, 0);
            cv::Mat ddy = shift_dy - dy[k];
            cv::Mat Normin2 = ddx + ddy;
            cv::Mat FNormin2;
            cv::dft(Normin2, FNormin2, cv::DFT_COMPLEX_OUTPUT);
            cv::Mat FS = Normin1[k] + beta*FNormin2;
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    FS.at<cv::Vec2d>(i,j)[0] /= Denormin.at<double>(i,j);
                    FS.at<cv::Vec2d>(i,j)[1] /= Denormin.at<double>(i,j);
                }
            }
            cv::Mat ifft;
            cv::idft(FS, ifft, cv::DFT_SCALE | cv::DFT_COMPLEX_OUTPUT);
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    single_channel[k].at<double>(i,j) = ifft.at<cv::Vec2d>(i,j)[0];
                }
            }
        }
        beta *= kappa;
        std::cout << '.';
    }
    cv::merge(single_channel, 3, S);
    
    cv::Mat rS;
    S.convertTo(rS, CV_8UC3, 255./1.);
    
    return rS;
}


void BlakeAlgorithm()
{
    float threshold;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);

    int width = img.cols;
    int height = img.rows;
    int channel = img.channels();
    img.convertTo(img, CV_32FC3, 1.0 / 255.0);
    
    // Obtain threshold value by keyborad interaction
    //cout << "[BlakeAlgorithm] input threshold value (default = 0.10): ";
    //cin >> threshold;
    
    threshold = 0.2;
    
    // Compute logarithm of input image
    cv::Mat out;
    clcnst::logarithm(img, out);
    
    // Laplacian filter divided by thresholding
    cv::Mat laplace = cv::Mat::zeros(height, width, CV_32FC3);
    for(int c=0; c<channel; c++) {
        // Compute gradient and thresholding
        cv::Mat grad = cv::Mat::zeros(height, width, CV_32FC2);
        for(int y=0; y<height-1; y++) {
            for(int x=0; x<width-1; x++) {
                float dx = out.at<float>(y, (x+1)*channel+c) - out.at<float>(y, x*channel+c);
                float dy = out.at<float>(y+1, x*channel+c) - out.at<float>(y, x*channel+c);
                if(fabs(dx) > threshold) {
                    grad.at<float>(y, x*2+0) = dx;
                }
                
                if(fabs(dy) > threshold) {
                    grad.at<float>(y, x*2+1) = dy;
                }
            }
        }
        
        // Compute gradient again
        for(int y=1; y<height; y++) {
            for(int x=1; x<width; x++) {
                float ddx = grad.at<float>(y, x*2+0) - grad.at<float>(y, (x-1)*2+0);
                float ddy = grad.at<float>(y, x*2+1) - grad.at<float>(y-1, x*2+1);
                laplace.at<float>(y, x*channel+c) = ddx + ddy;
            }
        }
    }
    
    // Gauss Seidel method
    clcnst::gauss_seidel(out, laplace, 20);
    
    // Normalization
    clcnst::normalize(out, out);
    
    // Compute exponential
    clcnst::exponential(out, out);
    
    // Display result
    cv::namedWindow("Input");
    cv::namedWindow("Output");
    cv::imshow("Input", img);
    cv::imshow("Output", out);
    
    // Save output
    //out.convertTo(out, CV_8UC3, 255.0);
    //cv::imwrite(ofname, out);
}

void HornAlgorithm()
{

    float threshold;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);
    
    int width = img.cols;
    int height = img.rows;
    int channel = img.channels();
    img.convertTo(img, CV_32FC3, 1.0 / 255.0);
    
    //cout << "[HornAlgorithm] input threshold value (default = 0.05): ";
    //cin >> threshold;
    
    threshold = 0.05;
    
    cv::Mat out, laplace;
    
    clcnst::logarithm(img, out);
    
    clcnst::laplacian(out, laplace);
    
    clcnst::threshold(laplace, laplace, threshold);
    
    clcnst::gauss_seidel(out, laplace, 20);
    
    clcnst::normalize(out, out);

    clcnst::exponential(out, out);
    
    cv::namedWindow("Input");
    cv::namedWindow("Output");
    cv::imshow("Input", img);
    cv::imshow("Output", out);
    
    // Save output
    //out.convertTo(out, CV_8UC3, 255.0);
    //cv::imwrite(ofname, out);
}


void MooreAlgorithm()
{
    float sigma;
    string isex;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);
    
    int width = img.cols;
    int height = img.rows;
    int channel = img.channels();
    img.convertTo(img, CV_32FC3, 1.0 / 255.0);
    
    // Apply Gaussian filter
    //cout << "[MooreAlgorithm] input sigma value for Gaussian: ";
    //cin >> sigma;
    
    sigma = 0.1;
    
    sigma = sigma * max(width, height);
    
    cv::Mat gauss;
    cv::GaussianBlur(img, gauss, cv::Size(0, 0), sigma);
    
    // Additional process for extended Moore
    //cout << "[MooreAlgorithm] use extended algorithm? (y/n): ";
    //cin >> isex;
    
    isex = "y";
    
    if(isex == "y") {
        cv::Mat gray;
        cv::cvtColor(img, gray, CV_BGR2GRAY);
        
        cv::Mat edge = cv::Mat::zeros(height, width, CV_32FC1);
        for(int y=1; y<height-1; y++) {
            for(int x=1; x<width-1; x++) {
                float dx = (gray.at<float>(y, x+1) - gray.at<float>(y, x-1)) / 2.0f;
                float dy = (gray.at<float>(y+1, x) - gray.at<float>(y-1, x)) / 2.0f;
                edge.at<float>(y, x) = sqrt(dx * dx + dy * dy);
            }
        }
        
        cv::GaussianBlur(edge, edge, cv::Size(0, 0), sigma);
        cv::namedWindow("Edge");
        cv::imshow("Edge", edge);
        
        for(int y=0; y<height; y++) {
            for(int x=0; x<width; x++) {
                for(int c=0; c<channel; c++) {
                    gauss.at<float>(y, x*channel+c) *= edge.at<float>(y, x);
                }
            }
        }
    }
    
    // Subtraction
    cv::Mat out;
    cv::subtract(img, gauss, out);
    
    // Offset reflectance
    out.convertTo(out, CV_32FC3, 1.0, -1.0);
    
    // Normalization
    clcnst::normalize(out, out, 0.0f, 1.0f);
    
    // Display result
    cv::namedWindow("Input");
    cv::namedWindow("Output");
    cv::imshow("Input", img);
    cv::imshow("Output", out);
    
    
    // Save output image
    //out.convertTo(out, CV_8UC3, 255.0);
    //cv::imwrite(ofname, out);
}

void RahmanAlgorithm()
{
    string isp;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);
    
    float sigma, ns, scale;
    
    int width = img.cols;
    int height = img.rows;
    int channel = img.channels();
    img.convertTo(img, CV_32FC3, 1.0 / 255.0);
    
    // Obtain parameters by keyboard interaction
    //cout << "[RahmanAlgorithm] you want to specify parameters? (y/n): ";
    //cin >> isp;
    
    isp = "n";
    
    if(isp == "y") {
        cout << "  sigma = ";
        cin >> sigma;
        cout << "  number of sigmas = ";
        cin >> ns;
        cout << "  scales for sigmas = ";
        cin >> scale;
    } else {
        sigma = 1.0f;
        ns = 3;
        scale = 0.16f;
    }
    
    vector<float> sigmas = vector<float>(ns);
    sigmas[0] = sigma * (float)max(height, width);
    for(int i=1; i<=ns; i++) sigmas[i] = sigmas[i-1] * scale;
    
    // Accumulate multiscale results of Moore's algorithm
    cv::Mat out, tmp, gauss;
    double weight = 0.0;
    out = cv::Mat(height, width, CV_32FC3);
    for(int i=0; i<ns; i++) {
        // Apply Gaussian filter
        cv::GaussianBlur(img, gauss, cv::Size(0, 0), sigmas[i]);
        
        // Subtraction
        cv::subtract(img, gauss, tmp);
        
        // Offset reflectance
        tmp.convertTo(tmp, CV_32FC3, 1.0, -1.0);
        
        // Normalization
        clcnst::normalize(tmp, tmp, 0.0f, 1.0f);
        
        // Accumulate
        cv::scaleAdd(tmp, 1.0 / (i+1), out, out);
        weight += 1.0 / (i+1);
    }
    out.convertTo(out, CV_32FC3, 1.0 / weight);
    
    // Display result
    cv::namedWindow("Input");
    cv::namedWindow("Output");
    cv::imshow("Input", img);
    cv::imshow("Output", out);

    
    // Save output image
    //out.convertTo(out, CV_8UC3, 255.0);
    //cv::imwrite(ofname, out);
}

void HomomorphicFilter()
{
    string isp;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);
    
    float lower, upper, threshold;
    
    int width = img.cols;
    int height = img.rows;
    int channel = img.channels();
    img.convertTo(img, CV_32FC3, 1.0 / 255.0);
    
    // Obtain parameters from command line arguments
    //cout << "[HomomorphicFilter] you want to specify parameters? (y/n): ";
    //cin >> isp;
    
    isp = "n";
    
    if(isp == "y") {
        cout << "  scale for  low frequency (default = 0.5): ";
        cin >> lower;
        cout << "  scale for high frequency (default = 2.0): ";
        cin >> upper;
        cout << "  threshold value for frequency domain (default = 7.5):";
        cin >> threshold;
    } else {
        lower = 0.5f;
        upper = 2.0f;
        threshold = 7.5f;
    }
    
    // Perform DFT, high emphasis filter and IDFT
    vector<cv::Mat> chs, spc(channel, cv::Mat(height, width, CV_32FC1));
    cv::split(img, chs);
    
    for(int c=0; c<channel; c++) {
        cv::dct(chs[c], spc[c]);
        clcnst::hef(spc[c], spc[c], lower, upper, threshold);
        cv::idct(spc[c], chs[c]);
    }
    cv::Mat out;
    cv::merge(chs, out);
    
    // Display result
    cv::namedWindow("Input");
    cv::namedWindow("Output");
    cv::imshow("Input", img);
    cv::imshow("Output", out);
    cv::waitKey(0);
    cv::destroyAllWindows();
    
    // Save result
    //out.convertTo(out, CV_8UC3, 255.0);
    //cv::imwrite(ofname, out);
}

void hef_faugeras(cv::Mat& input, cv::Mat& output) {
    cv::Mat* i_ptr = &input;
    int width = i_ptr->cols;
    int height = i_ptr->rows;
    int channel = i_ptr->channels();
    
    cv::Mat* o_ptr = &output;
    if(i_ptr != o_ptr) {
        *o_ptr = cv::Mat(height, width, CV_MAKETYPE(CV_32F, channel));
    }
    
    for(int y=0; y<height; y++) {
        for(int x=0; x<width; x++) {
            float r = sqrt((float)(x*x + y*y));
            double coeff = 1.5f - exp(- r / 5.0f);
            for(int c=0; c<channel; c++) {
                o_ptr->at<float>(y, x*channel+c) = coeff * i_ptr->at<float>(y, x*channel+c);
            }
        }
    }
}

void FaugerasAlgorithm()
{
    string isp;
    // Load input file
    cv::Mat img = cv::imread("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/"+ filename, CV_LOAD_IMAGE_COLOR);
    
    int width = img.cols;
	int height = img.rows;
	int channel = img.channels();
	img.convertTo(img, CV_32FC3, 1.0 / 255.0);
	
	// Convert color space 
	cv::Mat hvs;
	cv::cvtColor(img, hvs, CV_BGR2Lab);

	// Homomophic filtering
	vector<cv::Mat> chs, spc(channel, cv::Mat(height, width, CV_32FC1));
	cv::split(hvs, chs);

	for(int c=1; c<channel; c++) {
		cv::dct(chs[c], spc[c]);
		hef_faugeras(spc[c], spc[c]);
		cv::idct(spc[c], chs[c]);
	}
	cv::Mat out;
	cv::merge(chs, out);

	// Recover color space
	cv::cvtColor(out, out, CV_Lab2BGR);
	
	// Display result
	cv::namedWindow("Input");
	cv::namedWindow("Output");
	cv::imshow("Input", img);
	cv::imshow("Output", out);

	// Save result
	//out.convertTo(out, CV_8UC3, 255.0);
	//cv::imwrite(ofname, out);
}

int main(int argc, const char * argv[])
{
    
    cv::Mat im = cv::imread("/Users/dawang/Documents/Projects/L0smoothingCpp/image/"+ filename);
    cv::imshow("Before Smoothing", im);
    
    //cv::Mat image = L0Smoothing(im, 0.02,2);
    //cv::imshow("After SmoothiClssng parameter set for other", image);
    
    //cv::imwrite("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/pflower1.jpg", image);
    
    //cv::Mat image1 = L0Smoothing(im, 0.015,4);
    //cv::imshow("After SmoothiClssng parameter set for face", image1);
    
    //cv::imwrite("/Users/dawang/Documents/Projects/Github/L0smoothingCpp/image/pflower2.jpg", image1);
    
    //BlakeAlgorithm();
    //HornAlgorithm();
    //MooreAlgorithm();
    //RahmanAlgorithm();
    //HomomorphicFilter();
    //FaugerasAlgorithm();
    
    //LocalLaplacianFilter(filename);
    DomainTransform(filename);
    //ColorTransfer(filename, "grass.jpg");
    //SeamCarving(filename);
    
    // need to change the parameter, read in cvMat!!!
    //KmeansPlusPlus(filename, 8, 12);
    
    /*
    MorphoFeatures morpho;
    morpho.threshold = 40;
    
    cv::Mat grayimage;
    cv::cvtColor(im, grayimage, cv::COLOR_RGB2GRAY);
    
    cv::Mat edges;
    edges = morpho.getEdges(grayimage);
    //cv::imshow("Morpho Edge", edges);
    */
    
    // Graph Cut
    double sigma = 0.8;
    // the bigger k the less segmented part
    double k = 400.0;
    
    // small components removing
    int smallThr = 100;
    int segs = 0;
    
    
    //cv::Mat dst(image.rows, image.cols, CV_8UC3);
    //segs = graphCuts(image1, dst, sigma, k, smallThr);
    //printf("\nSegmented to %d parts...\n", segs);
    //imshow("Segmented Image", dst);
    
    //wait for the user to hit a key
    cvWaitKey(0);
    cv::destroyAllWindows();
    printf("done!\n");
    //return
    return 0;
}

