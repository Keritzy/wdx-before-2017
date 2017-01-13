function [ Fea ] = siftFea( I )
% 选出scale 最大的5个和最小的5个 descriptor作为feature
n = 5;
% 128(bins) * 10(10个点) * 3(RGB)

Fea = zeros(128,n*2,3);

for i = 1 : 3
    singleColorImage = single(I(:,:,i)) ;
    Fea(:,:,i) = singleColorSiftFea(singleColorImage,n);
end

% siftFea = siftFea(:);

end

function fea = singleColorSiftFea(I,n)
% The matrix f has a column for each frame. A frame is a disk of center f(1:2), 
% scale f(3) and orientation f(4) . We visualize a random selection of 50 features by:

    [f,d] = vl_sift(I) ;
    scale = f(4,:);
    [~,sortedIndex] = sort(scale);
    fea = [d(:,sortedIndex(1:n)),d(:,sortedIndex(end-n+1:end))];
end

