function [ gist ] = extractGIST( image, scale, block )
%UNTITLED4 Summary of this function goes here
%   Detailed explanation goes here

    img1 = uint8(reshape(image*255,[32,32,3]));

    param.imageSize = [32 32];
    param.orientationsPerScale = [8 8 8 8];
    param.numberBlocks = 4;
    param.fc_prefilt = 4;

    [gist, ~] = LMgist(img1, '', param);

end
