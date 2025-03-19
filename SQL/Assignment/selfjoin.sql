select tr.trainee_name as trainer, tn.trainee_name as trainee
from train as tr
join 
train as tn
on tr.trainee_id = tn.trainee_id 
