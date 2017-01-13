function [ nbModel ] = nbTrain( xTrain,yTrain )
% @param fea : n * f features
% @param lab : n * 1 class labels, >=1

% Resulting model : 
% @return M : f * c , Mean, each feature each class
% @return V : f * c , Variance, each feature each class
% @return p : c * 1 , prior probability of each class
yTrain = yTrain + 1;
c = max(yTrain);
f = size(xTrain,2);
M = zeros(f,c);
V = zeros(f,c);


for ci = 1 : c
    xc = xTrain((yTrain == ci),:);
    M(:,ci) = mean(xc,1);
    V(:,ci) = sqrt(var(xc,1));
end

nbModel.M = M;
nbModel.V = V;
nbModel.p = prior(yTrain);
end

