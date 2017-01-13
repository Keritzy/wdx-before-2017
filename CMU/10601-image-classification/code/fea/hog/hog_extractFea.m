function [ XFea ] = hog_extractFea( XImg )
cellSize = 4;
n = size(XImg,1);
XFea = zeros(n,1984);
for i = 1:size(XImg,1)
    im = im2single(reshape(XImg(i,:), 32, 32, 3));
    hog = vl_hog(im, cellSize);
    % XFea = [XFea;hog(:)'];
    XFea(i,:) = hog(:)';
end

end

