function [ Y ] = sfmClassify( softmaxModel, XTest )
%SFMCLASSIFY Summary of this function goes here
%   Detailed explanation goes here

[Y] = softmaxPredict(softmaxModel, XTest);
end

