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

/* Disjoint.h: Graph nodes segmentation class */

typedef struct {
	int rank;
	int p;
	int size;
}disjElem;

class disJoint{
public:
	disJoint(int elements);
	~disJoint();
	int find(int x);
	void join(int x, int y);
	int size(int x) const { return elts[x].size; }
	int num_sets() const { return num; }

private:
	disjElem *elts;
	int num;
};