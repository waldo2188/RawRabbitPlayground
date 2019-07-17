FROM rabbitmq:management-alpine

COPY config/rabbitmq.conf /etc/rabbitmq/rabbitmq.conf
COPY config/cfast_config.json /etc/rabbitmq/cfast_config.json

RUN chown rabbitmq /etc/rabbitmq/rabbitmq.conf /etc/rabbitmq/cfast_config.json
