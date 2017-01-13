%% Load the data, split it into train and test data
clear;
cd /Users/jinhongc/百度云同步盘/Documents/Projects/10601/prj.git/code
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


%% Parameter selection

Model = cell(100,1);
trainErr = zeros(100,1);
testErr = zeros(100,1);
Y = cell(100,1);
Acc = zeros(100,1);


% logf = fopen('nn.log','w');

s = sprintf('Node\t TrainErr\t TestErr\t Acc\n');
display(s);
% fprintf(logf,'%s',s);



for i = 1 : 25
    
    [Model{i},trainErr(i)] = train(dXTrain,YTrain,i);
    [Y{i},testErr(i)] = classify(Model{i},dXTest,YTest);
    
    cMat = confusionmat(YTest, uint8((Y{i})));
    Acc(i) = sum(diag(cMat))/sum(sum(cMat));
    
    s = sprintf('%d\t %f\t %f\t %f\t\n',...
                    i,trainErr(i),testErr(i),Acc(i));
    display(s);
    
    % fprintf(logf,'%s',s);
end

% fclose(logf);