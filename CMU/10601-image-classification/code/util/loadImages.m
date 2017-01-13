function [ imgs,labels,label_names ] = loadImages(color)
%LOADIMAGES Summary of this function goes here
%   Detailed explanation goes here
% [data,labels,label_names] = loadTrainData('data/train');
[data,labels,label_names] = loadTestData('data/test');
[N,~] = size(data);
if color
    imgs = zeros(32,32,3,N);
    for i = 1 : N
        imgs(:,:,:,i) = column2Image(data(i,:)); % colorful transform
    end
else
    imgs = zeros(32,32,N);
    for i = 1 : N
        imgs(:,:,i) = rgb2gray(column2Image(data(i,:))); % colorful transform
    end
end
clear data;

end

