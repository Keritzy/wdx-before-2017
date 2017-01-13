function [Model,trainErr] = train(XTrain,YTrain,option)



  %% feature extraction
  
  xTrainFea = extractFea(XTrain);
  

  
  %% Classifier

% naive bayes
% Model = nbTrain(xTrainFea,YTrain);


% logistic regression
% Model = mnrfit(double(xTrainFea),double(YTrain+1));

  
% svm
% Model = fitcsvm(double(xTrainFea),double(YTrain+1));

% softmax
% Model = sfmTrain(double(xTrainFea),double(YTrain+1));

% nn
Model = nnTrain(double(xTrainFea),double(YTrain+1),option);


%% Error

[~,trainErr] = classify(Model,XTrain,YTrain);

end
