function [ bow ] = extractBowSiftFea(imgs,visWord )
%EXTRACTBOWSIFTFEA Summary of this function goes here
%   Detailed explanation goes here


%{
% colorful
[X,Y,C,N] = size(imgs);
[~,K] = size(visWord);
bow = zeros(K,C,N);

parpool(4)
parfor i = 1 : N
    [ bow(:,:,i) ] = bowVisWord( imgs(:,:,:,i), visWord );
end
%}
[X,Y,N] = size(imgs);
[~,K] = size(visWord);
% bow = zeros(K,N);

bow = bowVisWord( imgs, visWord );
end

