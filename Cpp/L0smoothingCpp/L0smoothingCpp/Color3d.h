#ifndef _COLOR_3D_H_
#define _COLOR_3D_H_

#include <cassert>

class Color3d {
public:
	double v[3];

	Color3d();

	Color3d(double r, double g, double b);

	Color3d(const Color3d& c3d);

	Color3d& operator=(const Color3d& c3d);

	double& operator()(int i);

	Color3d operator+(const Color3d& c3d);

	Color3d operator-(const Color3d& c3d);

	Color3d operator*(const Color3d& c3d);

	Color3d multiply(double d);

	Color3d divide(double d);
};

#endif
