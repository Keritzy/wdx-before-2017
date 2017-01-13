function [ X,Y,C,N ] = getImagesDim( imgs )
% given images, return the dimension of color C, and number of images N

[X,Y,C,N] = size(imgs);
if C == 1 && N == 1
    return;
end
if N == 1 % if single color, the last dimension is singular
    N = C;
    C = 1;
end

end

