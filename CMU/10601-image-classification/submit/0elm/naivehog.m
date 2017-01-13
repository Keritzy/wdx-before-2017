function [ hog ] = naivehog(I,cellSize)
if nargin<2
    cellSize = 8;
end
I = im2single(I);
hog = vl_hog(I,cellSize);
hog = hog(:);
end

