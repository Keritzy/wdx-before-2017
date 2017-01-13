function [ Y ] = nnClassify(Model, XTest )
%NNCLASSIFY Summary of this function goes here
%   Detailed explanation goes here

Theta1 = Model.theta1;
Theta2 = Model.theta2;
% Useful values
m = size(XTest, 1);
num_labels = size(Theta2, 1);

% You need to return the following variables correctly 
% p = zeros(size(X, 1), 1);

h1 = sigmoid([ones(m, 1) XTest] * Theta1');
h2 = sigmoid([ones(m, 1) h1] * Theta2');
[~, Y] = max(h2, [], 2);

end

