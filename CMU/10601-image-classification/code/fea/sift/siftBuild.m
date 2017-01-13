function [ visWord ] = siftBuild( imgs, k )
% Extract visual word from images library
% @param I single precision input image library with dimentsion (X,Y,N),
% (X,Y) is the dimension of each image, N is the number of images in the
% library
% @param k the number of cluster to be performed on all the sift vector
% extracted from the image library
% @return visWord is a (128,k) matrix, each column represent a centroid of
% a cluster, with a total of k centroids.

[~,~,N] = size(imgs);

d = cell(N,1);
n = 0;

for i = 1 : N
    [~,d{i}] = vl_sift(single(imgs(:,:,i)));
    n = n + size(d{i},2);
end

D = zeros(128,n);
lower=1;

for i = 1 : N
    upper = size(d{i},2);
    D( : , lower:lower+upper-1 ) = d{i};
    lower = lower + upper;
end

%  [~,visWord] = kmeans(D,k);
[visWord,~] = vl_kmeans(D,k);

end

