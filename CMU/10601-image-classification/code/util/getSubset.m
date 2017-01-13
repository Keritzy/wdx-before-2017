function [ subset  ] = getSubset( data_set,num,classsize,classn,offset )
% all : f * n matrix, f dimension of features, n number of samples
% num : number of each class in the dataset to be extracted
% size: size of each class in the data
% classn : number of class in the data
% offset : the subset will be (offset:offset+num-1)

subset = zeros(size(data_set,1),classn*num);

for c = 1 : classn
    subset(:,(c-1)*num+1:c*num) = data_set(:,(c-1)*classsize+offset:(c-1)*classsize+offset+num-1);
end

end

