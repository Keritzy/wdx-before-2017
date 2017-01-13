function Model = ldaTrain(trainX, trainY, epsilon)

    if nargin < 3
        epsilon = 0.001;    
    end
    
    trainX = double(trainX);
    meanX = mean(trainX);
    
    miny = min(trainY);
    maxy = max(trainY);
    classCount = maxy - miny + 1;
    
    cov = bsxfun(@plus, trainX, -meanX);
    St = cov' * trainX;
    featureCount = size(trainX, 2);
    Sb = zeros(featureCount, featureCount);
    Model.centers = [];
    for i = miny:maxy
        cov = trainX(trainY == i,:);
        meanCov = mean(cov);
        Sb = Sb + size(cov,2) * (meanCov - meanX)' * (meanCov - meanX);
        Model.centers = [Model.centers; meanCov];
    end
    
    Sw = St - Sb;
    Sw = Sw + epsilon * eye(size(Sw));
    [V,~] = eig(Sw\Sb);
    Model.w = V(:,1:classCount-1);
    Model.centers = Model.centers * Model.w;
end
    
