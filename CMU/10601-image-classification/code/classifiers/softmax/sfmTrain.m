function [ Model ] = sfmTrain( XTrain,YTrain )
%SFMTRAIN Summary of this function goes here
%   Detailed explanation goes here
numClasses = size(unique(YTrain),1);
lambda = 1e-4;
labels = YTrain;
inputSize = size(XTrain,2);
inputData = XTrain';
[Model] = softmaxTrain(inputSize, numClasses, lambda, inputData, labels);

end

