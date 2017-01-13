function Model = train(XTrain,YTrain)


  %% feature extraction
  XTrain = extractFea(XTrain);
  

  
  %% Classifier

% naive bayes
% Model = nbTrain(xTrainFea,YTrain);


% logistic regression
% Model = mnrfit(double(xTrainFea),double(YTrain+1));

  
% svm
% Model = fitcsvm(double(xTrainFea),double(YTrain+1));

% softmax
% Model = sfmTrain(double(XTrain),double(YTrain+1));


% nn
Model = nnTrain(double(XTrain),double(YTrain+1));

end
