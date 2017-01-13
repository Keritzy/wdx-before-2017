function [pred] = softmaxPredict(softmaxModel, data)
theta = softmaxModel.theta;
M = exp(theta*data);
M = bsxfun(@rdivide, M, sum(M));

[~, pred] = max(M);

end

