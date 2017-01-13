function [mdl] = logrTrain(xTrain, yTrain)
step = 0.1;
w = ones(size(xTrain,2),1);
sigma = 0; % regularization term
nIter = 1000;
% l = zeros(nIter,1);
% i = 1;
while nIter ~= 0
    w = w - step .* sigma .* w + step .* gloglikelihood(xTrain,yTrain,w);
    
%     l(i) = lossfun(xTrain,yTrain,w);
%     i = i + 1;
    
    nIter = nIter - 1;
end

mdl.w = w;
end


function gw = gloglikelihood(x,y,w) % gw : 
% gw: (f+1) * 1, gradient loglikelihood wrt. w
% x : n * f
% y : n * 1
[n,f] = size(x);
y = double(y);
gw = sum(x .* repmat(y - sigmoidProb(ones(n,1), x, w),1,f),1)';
% gw0 = sum(y - sigmoidProb(ones(n,1), x, w),1);
% gw = [gw0; gw];
end

function l = lossfun(xTrain,yTrain,w)
    l = sum((yTrain - logisticRegressionClassify(xTrain, w)).^2);
end