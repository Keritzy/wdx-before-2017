function [ Model ] = nnTrain( XTrain,YTrain, option )

input_layer_size  = size(XTrain,2);
hidden_layer_size = 25;
if exist('option','var')
    hidden_layer_size = option;
end
num_labels = size(unique(YTrain),1);

initial_Theta1 = randInitializeWeights(input_layer_size,hidden_layer_size);
initial_Theta2 = randInitializeWeights(hidden_layer_size,num_labels);


%{
initial_Theta1 = rand(hidden_layer_size, 1 + input_layer_size );
initial_Theta2 = rand(num_labels, 1 + hidden_layer_size );
%}

initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];
options = optimset('Display','off','GradObj', 'on','MaxIter', 150);

lambda = 1; % regularization parameter

costFunction = @(p) nnCostFunction(p, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, XTrain, YTrain, lambda);
% display(sprintf('Training nn: \n\t hidden node: %d \n\t MaxIter: %d\n',hidden_layer_size,150));
                               
[nn_params, cost] = minFunc(costFunction, initial_nn_params, options);

Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));

Model.theta1 = Theta1;
Model.theta2 = Theta2;
             
end

