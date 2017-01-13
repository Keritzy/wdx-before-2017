function [data,labels,label_names] = loadTrainData( directory )
%LOADTESTDATA Summary of this function goes here
%   Detailed explanation goes here

fprintf('Loading training data...\n');
f0=load([directory '/batches.meta.mat']);
label_names = f0.label_names;

f1=load([directory '/small_data_batch_1.mat']);
f2=load([directory '/small_data_batch_2.mat']);
f3=load([directory '/small_data_batch_3.mat']);
f4=load([directory '/small_data_batch_4.mat']);
f5=load([directory '/small_data_batch_5.mat']);

data = double([f1.data; f2.data; f3.data; f4.data; f5.data]);%50000*3072
labels = double([f1.labels; f2.labels; f3.labels; f4.labels; f5.labels]) + 1; % add 1 to labels!,??1?10

clear f0 f1 f2 f3 f4 f5;
fprintf('Loading Done\n');


end

