function [cls] = classify(Model, xTest)
%KMEANSCLASSIFY Summary of this function goes here
%   Detailed explanation goes here
rfSize = 6;
CIFAR_DIM=[32 32 3];
M = Model.m;
P = Model.p;
theta = Model.theta;
centroids = Model.centroids;
trainXC_mean = Model.trainXC_mean;
trainXC_sd = Model.trainXC_sd;


addpath minFunc

testXC = extractFeatures(xTest, centroids, rfSize, CIFAR_DIM, M,P);

testXCs = bsxfun(@rdivide, bsxfun(@minus, testXC, trainXC_mean), trainXC_sd);
testXCs = [testXCs, ones(size(testXCs,1),1)];

% test and print result
[val,labels] = max(testXCs*theta, [], 2);

cls = labels;

end

% fprintf('Test accuracy %f%%\n', 100 * (1 - sum(labels ~= testY) / length(testY)));
