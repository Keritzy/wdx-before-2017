function [ Model ] = nnTrain( XTrain,YTrain )

input_layer_size  = size(XTrain,2);
hidden_layer_size = 25;
num_labels = size(unique(YTrain),1);

initial_Theta1 = randInitializeWeights(input_layer_size,hidden_layer_size);
initial_Theta2 = randInitializeWeights(hidden_layer_size,num_labels);


%{
initial_Theta1 = rand(hidden_layer_size, 1 + input_layer_size );
initial_Theta2 = rand(num_labels, 1 + hidden_layer_size );
%}

initial_nn_params = [initial_Theta1(:) ; initial_Theta2(:)];
options = optimset('Display','iter','GradObj', 'on','MaxIter', 50);

lambda = 1; % regularization parameter

costFunction = @(p) nnCostFunction(p, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, XTrain, YTrain, lambda);
[nn_params, cost] = minFunc(costFunction, initial_nn_params, options);

% [nn_params, cost] = fminsearch(costFunction, initial_nn_params, options);

Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));

Model.theta1 = Theta1;
Model.theta2 = Theta2;
             
end

function W = randInitializeWeights(L_in, L_out)
epsilong = 0.12;
W = rand(L_out, 1 + L_in) * 2 * epsilong - epsilong;
end


function [J grad] = nnCostFunction(nn_params, ...
                                   input_layer_size, ...
                                   hidden_layer_size, ...
                                   num_labels, ...
                                   X, y, lambda)
                               
Theta1 = reshape(nn_params(1:hidden_layer_size * (input_layer_size + 1)), ...
                 hidden_layer_size, (input_layer_size + 1));

Theta2 = reshape(nn_params((1 + (hidden_layer_size * (input_layer_size + 1))):end), ...
                 num_labels, (hidden_layer_size + 1));

% Setup some useful variables
m = size(X, 1);
      

Theta1_grad = zeros(size(Theta1));
Theta2_grad = zeros(size(Theta2));

z2 = [ones(m,1),X] * Theta1'; % input to the hidden layer
a2 = sigmoid(z2);             % activation of hidden layer
z3 = [ones(m,1),a2] * Theta2';% input to the output layer
h  = sigmoid(z3); clear z3;   % output of the output layer

% error
Y = zeros(size(h));
for j = 1 : m
    Y(j,y(j)) = 1;
end
summat = -Y.*log(h) - (1-Y) .* log(1-h); clear Y;
J = 1 / m * sum(summat(:)); clear summat;

% regularization
Theta1sq = Theta1(:,2:end).^2;
Theta2sq = Theta2(:,2:end).^2;

J_reg = lambda/(2*m) * (sum(Theta1sq(:)) + sum(Theta2sq(:)));
clear Theta1sq Theta2sq;

J = J + J_reg;

% back propagation

for t = 1 : m
    delta_3 = h(t,:)';
    delta_3(y(t)) = delta_3(y(t)) - 1;
    % delta3 get!
    delta_2 = Theta2' * delta_3 .* sigmoidGradient([1,z2(t,:)]');
    % delta2 get!
    
    % accumulate Delta_3 and Delta_2
    Theta2_grad = Theta2_grad + delta_3 * [1,a2(t,:)];
    Theta1_grad = Theta1_grad + delta_2(2:end) * [1,X(t,:)];
end

Theta2_grad = Theta2_grad ./ m;
Theta1_grad = Theta1_grad ./ m;


Theta2_grad(:,2:end) = Theta2_grad(:,2:end) + lambda/m .* Theta2(:,2:end);
Theta1_grad(:,2:end) = Theta1_grad(:,2:end) + lambda/m .* Theta1(:,2:end);

% Unroll gradients
grad = [Theta1_grad(:) ; Theta2_grad(:)];


end


function g = sigmoid(z)
%SIGMOID Compute sigmoid functoon
%   J = SIGMOID(z) computes the sigmoid of z.

g = 1.0 ./ (1.0 + exp(-z));
end

function g = sigmoidGradient(z)
g = sigmoid(z) .* (1 - sigmoid(z));
end
