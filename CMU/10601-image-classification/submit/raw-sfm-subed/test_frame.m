%% Load the data, split it into train and test data
clc;clear;
addpath(genpath('testframe'),genpath('data'),genpath('classifiers'),...
    genpath('lib'),genpath('fea'));

XTrain = [];
YTrain = [];

load small_data_batch_1
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_2
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_3
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_4
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_5
XTest = data;
YTest = labels;

dXTrain = double(XTrain);
dXTest = double(XTest);

%% Train
name = input('Please input name of the model:\n');
Model = train(XTrain,YTrain);
save(['Model',name,'-',datestr(now),'.mat'],'Model');

%% Classify
Y     = classify(Model,dXTest);
cMat = confusionmat(YTest, uint8((Y)));
Acc = sum(diag(cMat))/sum(sum(cMat));

display(sprintf('%f',Acc));