//-------------------------------------------------------------//
//
// * Graph Cuts Segmentation with OpenCV
//   In "Efficient Graph-Based Image Segmentation" 
//   by Pedro Felzenszwalb & Daniel P. Huttenlocher (2004)
//
//-------------------------------------------------------------//
//
// * Date: 2014.04.16
// * Coder: Mark Lee(tefactory@live.com)
// * OpenCV Version: 2.4.8
// * Original Source: http://cs.brown.edu/~pff/segment/
//
//-------------------------------------------------------------//

/*
 sigma: Used to smooth the input image before segmenting it.
 k: Value for the threshold function.
 smallThr: Minimum component size enforced by post-processing.
 
 
 Typical parameters are sigma = 0.5, k = 500, smallThr = 20.
 Larger values for k result in larger components in the result.
 */

/* Graphcuts.h: Graph process and segmentation */

#include <cstdlib>
#include <algorithm>
#include <cmath>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2/imgproc/imgproc.hpp>

#include "Disjoint.h"

using namespace cv;

#define THRESHOLD(size, smallThr) (smallThr/size)

template <class T>
inline T square(const T &x) { return x*x; };

typedef unsigned char uchar;

typedef struct {
	double w;
	int a, b;
} edge;

bool operator<(const edge &a, const edge &b);

int graphCuts(Mat& src, Mat& dst, double sigma, double c, int smallThr);
static inline double dissim(Mat &red, Mat &green, Mat &blue,
							int x1, int y1, int x2, int y2);
disJoint *segGraph(int num_vertices, int num_edges, edge* edges, double c);