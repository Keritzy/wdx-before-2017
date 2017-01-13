function [p] = prior(yTrain)
% calculate the prior probability by counting the frequency of the labels
% divided by the number of the sample data;

% @param  yTrain : labels of training data, >= 1
% @return p      : c * 1 , prior probability fo class P(class=i)
cm = max(yTrain);
p = zeros(cm,1);
len = size(yTrain,1);
for i = 1 : cm
    p(i) = sum(yTrain == i) / len;
end

end
