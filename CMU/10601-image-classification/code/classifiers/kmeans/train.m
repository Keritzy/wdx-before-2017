function [Model] = train(xTrain, yTrain)

%% Configuration
addpath minFunc;
rfSize = 6;
numCentroids=1600; % size of centoirds
numPatches = 400000;
CIFAR_DIM=[32 32 3];

% extract random patches
patches = zeros(numPatches, rfSize*rfSize*3);
for i=1:numPatches
  if (mod(i,10000) == 0)
      fprintf('Extracting patch: %d / %d\n', i, numPatches); end

  % unified distribution
  r = random('unid', CIFAR_DIM(1) - rfSize + 1);
  c = random('unid', CIFAR_DIM(2) - rfSize + 1);
  % for each image, get numPatches/size(trainX,1) patches
  patch = reshape(xTrain(mod(i-1,size(xTrain,1))+1, :), CIFAR_DIM);
  patch = patch(r:r+rfSize-1,c:c+rfSize-1,:);
  % each line represents a small sample
  patches(i,:) = patch(:)';
end

% normalize for contrast
% subtract the mean and divide by std (std + 10)
% bsxfun(@rdivide,A,B): element in A divide by corresponding B
patches = bsxfun(@rdivide, bsxfun(@minus, patches, mean(patches,2)), sqrt(var(patches,[],2)+10));

% whiten
% the covariance matrix for patches
C = cov(patches);
M = mean(patches);
[V,D] = eig(C);
P = V * diag(sqrt(1./(diag(D) + 0.1))) * V';
% the mean of each dimension should be zero
% the position of whitening matrix are different
patches = bsxfun(@minus, patches, M) * P;


% run K-means
centroids = getCentroids(patches, numCentroids, 50);

% extract training features

% M is mean vector, P is whitening mat
% CIFAR_DIM is the dimension of patches
% rfSize is the size of each patch
trainXC = extractFeatures(xTrain, centroids, rfSize, CIFAR_DIM, M,P);


% standardize data
trainXC_mean = mean(trainXC);
trainXC_sd = sqrt(var(trainXC)+0.01);
trainXCs = bsxfun(@rdivide, bsxfun(@minus, trainXC, trainXC_mean), trainXC_sd);
% add 1 to each feature
trainXCs = [trainXCs, ones(size(trainXCs,1),1)];

% train classifier using SVM
C = 100;
theta = trainSVM(trainXCs, yTrain, C);

[val,labels] = max(trainXCs*theta, [], 2);
fprintf('Train accuracy %f%%\n', 100 * (1 - sum(labels ~= yTrain) / length(yTrain)));


Model.theta = theta;
Model.m = M;
Model.p = P;
Model.centroids = centroids;
Model.trainXC_mean = trainXC_mean;
Model.trainXC_sd = trainXC_sd;

save('Model.mat', Model);

end

