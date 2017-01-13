function [data,labels,label_names] = loadTestData( directory )
%LOADTESTDATA Summary of this function goes here
%   Detailed explanation goes here

fprintf('Loading test data...\n');
f1=load([directory '/test_batch.mat']);
label_names = f1.label_names;
data = double(f1.data);
labels = double(f1.labels) + 1;
clear f1;

fprintf('Loading Done\n');

end

