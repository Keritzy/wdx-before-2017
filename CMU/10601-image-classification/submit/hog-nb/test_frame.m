%% Load the data, split it into train and test data
clc;clear;
addpath(genpath('testframe'),genpath('data'),genpath('classifiers'),...
    genpath('lib'),genpath('fea'));

XTrain = [];
YTrain = [];

load data_batch_1
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load data_batch_2
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load data_batch_3
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load data_batch_4
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load data_batch_5
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load test_batch
XTest = data;
YTest = labels;

dXTrain = double(XTrain);
dXTest = double(XTest);

%% Train
Model = train(XTrain,YTrain);

%% Classify
Y     = classify(Model,dXTest);
cMat = confusionmat(YTest, uint8((Y)));
Acc = sum(diag(cMat))/sum(sum(cMat));

display(sprintf('%f',Acc));

name = input('Please input name of the model:\n');
save(['models/Model-',name,'-',datestr(now),'.mat'],'Model');
% Node Accuracy
% 100  0.544
% 300  0.544