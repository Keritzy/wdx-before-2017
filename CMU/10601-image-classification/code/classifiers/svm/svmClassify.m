function [ Y ] = svmClassify( Model,X )
%SVMCLASSIFY Summary of this function goes here
%   Detailed explanation goes here
Y = predict(Model,X);

end

