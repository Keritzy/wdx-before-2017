function [softmaxModel] = softmaxTrain(inputSize, numClasses, lambda, inputData, labels, options)

if ~exist('options', 'var')
    options = struct;
end

if ~isfield(options, 'maxIter')
    options.maxIter = 1000;
end

% initialize parameters
theta = 0.005 * randn(numClasses * inputSize, 1);

options.Method = 'lbfgs';
options.display = 'on';
% options.displayImage = false;

[softmaxOptTheta, cost] = minFunc( @(p) softmaxCost(p, ...
                                   numClasses, inputSize, lambda, ...
                                   inputData, labels), ...                                   
                              theta, options);


softmaxModel.theta = reshape(softmaxOptTheta, numClasses, inputSize);
softmaxModel.inputSize = inputSize;
softmaxModel.numClasses = numClasses;
                          
end                          