%% Load the data, split it into train and test data
clear;
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
Model = train(XTrain,YTrain);

%% Classify
Y     = classify(Model,dXTest);
cMat = confusionmat(YTest, uint8((Y)));
Acc = sum(diag(cMat))/sum(sum(cMat));

display(sprintf('%f',Acc));
%{
name = input('Please input name of the model:\n');
save(['Model',name,'-',datestr(now),'.mat'],'Model');
%}