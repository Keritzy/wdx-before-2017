function Model = trainFisher(data,label,epsilon)
    if nargin < 3
        epsilon = 0.001;
    end

    data = double(data);
    m = mean(data);
    n = max(label)-min(label) + 1;
    cdata = bsxfun(@plus,data,-mean(data));
    St = cdata'*data;
    Sb = zeros(size(data,2),size(data,2));
    Model.Centers = [];
    for i = min(label) : max(label)
        cdata = data(label == i,:);
        mc = mean(cdata);
        Sb = Sb + size(cdata,2)*(mc-m)'*(mc-m);
        Model.Centers = [Model.Centers;mc];
    end
    Sw = St-Sb;
    Sw = Sw + epsilon*eye(size(Sw));
    [V,~] = eig(Sw\Sb);
    Model.W = V(:,1:n-1);
    Model.Centers = Model.Centers * Model.W;
end

