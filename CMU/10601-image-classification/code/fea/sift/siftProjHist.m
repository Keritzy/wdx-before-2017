function [ hist ] = siftProjHist( img,visWord )
% Compute the histogram of an image I on visWord
% @param visWord is a (128,K,N) 3D array, K is the number of cluster
% centroids to perform histogram counting, and N is the dimension of color
% @param I is a (X,Y,N) image with (X,Y) in space dimension and N dimension
% in color
% @return hist is a (K,N) matrix
[X,Y,N] = size(img);
[D,K,N] = size(visWord);
hist = zeros(K,N);


for i = 1 : N
    [~,siftFea] = vl_sift(single(img(:,:,i)));
    hist(:,i) = calHist(siftFea,visWord(:,:,i));
    hist(:,i) = normalizedHist(hist(:,i));
end

hist = hist(:);

end

function hist = calHist(siftFea,visWord)
    [D,K] = size(visWord);
    [~,N] = size(siftFea);
    hist = zeros(K,1);
    
    for i = 1 : N
        [~,minInd] = min(sum((double(repmat(siftFea(:,i),1,K)) - visWord)...
            .^2,1));
        hist(minInd) = hist(minInd) + 1;
    end
end


function hist = normalizedHist(hist)
    meanValue = mean(hist);
    minimum = min(hist);
    maximum = max(hist);
    hist = (hist - meanValue) ./ (maximum - minimum);
end
