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

/* Disjoint.cpp: Graph nodes segmentation class */

#include "Disjoint.h"

// constructor
disJoint::disJoint(int elements){
	elts = new disjElem[elements];
	num = elements;

	for (int i = 0; i < elements; i++) {
		elts[i].rank = 0;
		elts[i].size = 1;
		elts[i].p = i;
	}
}

// destructor -memory free
disJoint::~disJoint() {
	delete[] elts;
}

// edge compare
int disJoint::find(int x){
	int y = x;
	while (y != elts[y].p){
		y = elts[y].p;
	}
	elts[x].p = y;
	return y;
}

// reconnect
void disJoint::join(int x, int y){
	if (elts[x].rank > elts[y].rank){
		elts[y].p = x;
		elts[x].size += elts[y].size;
	}
	else{
		elts[x].p = y;
		elts[y].size += elts[x].size;
		if (elts[x].rank == elts[y].rank){
			elts[y].rank++;
		}
	}
	num--;
}