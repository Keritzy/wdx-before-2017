function [Model] = elmTrain(trainX, trainY, NeuronNumber, Kernel)

Y = trainY';
X = trainX';

dataCount = size(X,2);
inputNeuronCount = size(X,1);

% preprocessing
newLabel = sort(Y,2);
label = zeros(1,1);

label(1,1) = newLabel(1,1);
j = 1;
for i = 2:dataCount
    if newLabel(1,i) ~= label(1,j)
        j=j+1;
        label(1,j) = newLabel(1,i);
    end
end
classCount = j;
outputNeuronCount = classCount;

tempY = zeros(outputNeuronCount, dataCount);
for i = 1:dataCount
    for j = 1:classCount
        if label(1,j) == Y(1,i)
            break;
        end
    end
    tempY(j,i) = 1;
end
Y = tempY*2-1;

inputWeight = rand(NeuronNumber, inputNeuronCount)*2-1;
hiddenNeuronBias = rand(NeuronNumber,1);
tempH = inputWeight*X;

clear X;

ind = ones(1, dataCount);
biasMat = hiddenNeuronBias(:,ind);
tempH = tempH + biasMat;

switch Kernel
    case {'sig'}
        H = 1./ (1 + exp(-tempH));
    case {'sin'}
        H = sin(tempH);
    case {'hardlim'}
        H = hardlim(tempH);
    case {'tribas'}
        H = tribas(tempH);
    case {'radbas'}
        H = radbas(tempH);
end
clear tempH;

outputWeight = pinv(H') * Y';
clear H;

Model.inputWeight = inputWeight;
Model.outputWeight = outputWeight;
Model.hiddenNeuronBias = hiddenNeuronBias;
Model.kernel = Kernel;
