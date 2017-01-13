function [ visWord ] = buildSiftVisWord(imgs,k)
%BUILDSIFTVISWORD Summary of this function goes here
%   Detailed explanation goes here

%{
% colorful build

visWord = zeros(128,k,3);
parpool(4)
parfor i = 1 : 3
    visWord(:,:,i) = siftVisWord( single(imgs(:,:,i,:)) ,k);
end

%}

visWrod = siftVisWrod( single(imgs,k) );

end

