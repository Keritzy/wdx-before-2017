function [cls] = logrClassify(xTest, mdl)
% Classifier using logistic regression
% @param xTest : n * f testing data
% @param w     : f * 1 weights of the logitics regression model
% @return cls  : n * 1 classification result labels, >= 1

w = mdl.w;
n = size(xTest,1);
p = [sigmoidProb(zeros(n,1), xTest, w),sigmoidProb(ones(n,1), xTest, w)];
[~,cls] = max(p,[],2);

% cls = cls - 1;
end
