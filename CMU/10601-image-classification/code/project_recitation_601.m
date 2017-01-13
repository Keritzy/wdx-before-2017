%% Load the data, split it into train and test data
XTrain = [];
YTrain = [];

load small_data_batch_1
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_2
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_3
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_4
XTrain = [XTrain;data];
YTrain = [YTrain;labels];

load small_data_batch_5
XTest = data;
YTest = labels;

dXTrain = double(XTrain);
dXTest = double(XTest);

%% Train the data using the intensity values as features
NBModel = fitNaiveBayes(dXTrain, YTrain);
Y = NBModel.predict(dXTest);
cMat = confusionmat(YTest, Y);
intensityAcc = sum(diag(cMat))/sum(sum(cMat));

%% Setup VLFeat
run('vlfeat-0.9.20/toolbox/vl_setup');

%% Uncomment the following lines to visualize the HoG features
% cellSize = 8;
% I = im2single(imread('link.jpg'));
% hog = vl_hog(I, cellSize, 'verbose');
% imhog = vl_hog('render', hog, 'verbose') ;
% clf ; imagesc(imhog) ; colormap gray ;

%% Uncomment the following lines to visualize gradients
% [Gmag, Gdir] = imgradient(rgb2gray(imread('link.jpg')), 'prewitt');
% figure; imshowpair(Gmag, Gdir, 'montage');

%% Extract HoG features
cellSize = 4;

XTrainFeat = [];
for i = 1:size(XTrain,1)
    im = im2single(reshape(XTrain(i,:), 32, 32, 3));
    hog = vl_hog(im, cellSize);
    XTrainFeat = [XTrainFeat;hog(:)'];
end

XTestFeat = [];
for i = 1:size(XTest,1)
    im = im2single(reshape(XTest(i,:), 32, 32, 3));
    hog = vl_hog(im, cellSize);
    XTestFeat = [XTestFeat;hog(:)'];
end

%% Train on HoG features
NBModel = fitNaiveBayes(XTrainFeat, YTrain);
Y = NBModel.predict(XTestFeat);
cMat = confusionmat(YTest, Y);
hogAcc = sum(diag(cMat))/sum(sum(cMat));
